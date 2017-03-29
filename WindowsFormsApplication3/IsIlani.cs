using System;

namespace WindowsFormsApplication3
{
   
    public class IsIlani : ICloneable
    {

        public String SirketIsim { get; set; }
        public String SirketAdres { get; set; }
        public String SirketTel { get; set; }
        public String SirketEposta { get; set; }
        public String SirketFaks { get; set; }
        public String ilanTanim { get; set; }
        public String kriterler { get; set; }
        public Heap adaylar = new Heap(20);

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public HeapNode enUygunAdaySec()
        {
            return adaylar.Remove();
        }
    }
}
