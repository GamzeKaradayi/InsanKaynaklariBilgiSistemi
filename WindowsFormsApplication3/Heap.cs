namespace WindowsFormsApplication3
{
    public class Heap
    {
        public HeapNode[] heapArray;
        private int maxSize;
        private int currentSize; 

        public Heap(int maxHeapSize)
        {
            maxSize = maxHeapSize;
            currentSize = 0;
            heapArray = new HeapNode[maxSize];
        }
        public bool IsEmpty()
        { return currentSize == 0; }
        public bool Insert(HeapNode value)
        {
            if (currentSize == maxSize)
                return false;
            HeapNode newNode = value;
            heapArray[currentSize] = newNode;
            CascadeUp(currentSize++);
            return true;
        }

        public void CascadeUp(int index)
        {
            int parent = (index - 1) / 2;
            HeapNode bottom = heapArray[index];
            while (index > 0 && heapArray[parent].Uygunluk < bottom.Uygunluk)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }
        public HeapNode Remove() 
        {
            HeapNode root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            CascadeDown(0);
            return root;
        }
        
        public void CascadeDown(int index)
        {
            int largerChild;
            HeapNode top = heapArray[index];
            while (index < currentSize / 2)
            {
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                if (rightChild < currentSize && heapArray[leftChild].Uygunluk < heapArray[rightChild].Uygunluk)
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                if (top.Uygunluk >= heapArray[largerChild].Uygunluk)
                    break;
                heapArray[index] = heapArray[largerChild];
                index = largerChild;
            }
            heapArray[index] = top;
        }


    }
}
