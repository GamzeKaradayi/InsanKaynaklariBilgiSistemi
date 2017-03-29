namespace WindowsFormsApplication3
{

    class HashMap
    {
        int TABLE_SIZE = 10;

        public LinkedHashEntry[] table;

        public HashMap()
        {
            table = new LinkedHashEntry[TABLE_SIZE];
            for (int i = 0; i < TABLE_SIZE; i++)
                table[i] = null;
        }
        public IsIlani GetIlan(int key)
        {
            int hash = (key % TABLE_SIZE);
            if (table[hash] == null)
                return null;
            else {
                LinkedHashEntry entry = table[hash];
                while (entry != null && entry.Anahtar != key)
                    entry = entry.Next;
                if (entry == null)
                    return null;
                else
                    return (IsIlani)entry.Deger;
            }
        }

        public void AddIlan(int key, object value)
        {
            
            int hash = (key % TABLE_SIZE);
            if (table[hash] == null)
                table[hash] = new LinkedHashEntry(key, value);
            else {
                LinkedHashEntry entry = table[hash];
                while (entry.Next != null && entry.Anahtar != key)
                    entry = entry.Next;
                if (entry.Anahtar == key)
                    entry.Deger = value;
                else
                    entry.Next = new LinkedHashEntry(key, value);
            }
        }
        public void RemoveIlan(int key)
        {
            int hash = (key % TABLE_SIZE);
            if (table[hash] != null)
            {
                LinkedHashEntry prevEntry = null;
                LinkedHashEntry entry = table[hash];
                while (entry.Next != null && entry.Anahtar != key)
                {
                    prevEntry = entry;
                    entry = entry.Next;
                }
                if (entry.Anahtar == key)
                {
                    if (prevEntry == null)
                        table[hash] = entry.Next;
                    else
                        prevEntry.Next = entry.Next;
                }
            }
        }
    }
}
