using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPrototype
{
    public class Save
    {
        string saveName;
        int statementNumber;

        public int StatementNumber { get { return statementNumber; } }

        public Save(string saveName, int statementNumber)
        {
            this.saveName = saveName;
            this.statementNumber = statementNumber;
        }

        public static void SaveGameFile(Save save)
        {
            File.WriteAllText(@"..\..\resources\" + save.saveName + ".json", JsonConvert.SerializeObject(save, Formatting.Indented));
        }
        public static Save LoadGameFile(string saveName)
        {
            return JsonConvert.DeserializeObject<Save>(File.ReadAllText(@"..\..\resources\" + saveName + ".json"));
        }
    }
}
