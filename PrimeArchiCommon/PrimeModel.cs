using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeArchiCommon.PrimeModel
{
    public class PayloadList
    {
#pragma warning disable IDE1006 // Naming Styles
     //   public IList<PrimeSystem> primeSystems;
        public IList<IList<PrimeSystem>> payload { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    public class PrimeSystem
    {
        string id;
        string name;
        string description;
        bool isPhysical;
        string[] tags;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool IsPhysical { get => isPhysical; set => isPhysical = value; }
        public string[] Tags { get => tags; set => tags = value; }
    }
}