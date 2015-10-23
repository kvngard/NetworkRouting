using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    public class PriorityQueue
    {
        private List<node> heap = new List<node>();
        private List<node> pointers = new List<node>();
        private int degree = 2;

        public PriorityQueue(int numPoints, int? degree = null, bool onePath = false)
        {
            pointers = new List<node>( new node[numPoints] );

            if (onePath == false)
            {
                for (int i = 0; i < numPoints; i++)
                {
                    insert(i, double.PositiveInfinity);
                }
            }

            if (degree != null)
            {
                this.degree = (int)degree;
            }
        }

        private int getParentIndex(int index)
        {
            return (int)Math.Floor((double)((index - 1) / degree)); 
        }

        private int getChildSmallest(int index)
        {
            int childIndex = (degree * index) + 1;
            if (childIndex > heap.Count - 1)
                return index;

            for(int i = 0; i < degree; i++)
            {
                if ((degree * index) + 1 + i > heap.Count - 1)
                    break;

                double childDistance = heap[(degree * index) + 1 + i].value;
                if (childDistance < heap[childIndex].value)
                    childIndex = (degree * index) + 1 + i;
            }
            return childIndex;
        }

        private void swap(int aIndex, int bIndex)
        {
            heap[aIndex].index = bIndex;
            heap[bIndex].index = aIndex;

            node temp = heap[aIndex];
            heap[aIndex] = heap[bIndex];
            heap[bIndex] = temp;
        }

        private void swapUp(int childIndex)
        {
            int parentIndex = getParentIndex(childIndex);

            while (heap[parentIndex].value > heap[childIndex].value && childIndex != 0)
            {
                swap(childIndex, parentIndex);
                childIndex = parentIndex;
                parentIndex = getParentIndex(childIndex);
            }
        }

        private void swapDown(int parentIndex)
        {
            int childIndex = getChildSmallest(parentIndex);

            while (heap[childIndex].value < heap[parentIndex].value)
            {
                swap(childIndex, parentIndex);
                parentIndex = childIndex;
                childIndex = getChildSmallest(parentIndex);
            }
        }

        public node insert(int id, double distance, node prev = null)
        {
            node n = new node(id, distance, heap.Count);
            n.prev = prev;
            heap.Add(n);
            pointers[id] = n;

            swapUp(n.index);

            return n;
        }

        public node insert(node n, node prev)
        {
            if (n.index > heap.Count)
                n.index = heap.Count;

            n.prev = prev;
            heap.Add(n);
            pointers[n.id] = n;

            swapUp(n.index);

            return n;
        }

        public void update(int id, double distance, node prev)
        {
            pointers[id].value = distance;
            pointers[id].prev = prev;
            swapUp(pointers[id].index);
        }

        public node pop()
        {
            if (heap.Count() > 0)
            {
                node least = heap[0];
                swap(0, heap.Count - 1);

                pointers[heap[0].id].index = -1;
                heap.RemoveAt(heap.Count - 1);

                if(heap.Count > 1)
                    swapDown(0);

                return least;
            }

            return null;
        }

        public bool isNotEmpty()
        {
            if (heap.Count > 0)
                return true;
            return false;
        }

        public node find(int index)
        {
            if (index > pointers.Count - 1)
                return null;
            return pointers[index];
        }

        public node peek()
        {
            return heap[0];
        }

    }
}
