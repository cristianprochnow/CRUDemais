using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRUDemais
{
    class LocalDatabase
    {
        private static LocalDatabase? instance;
        public static List<FatoCurioso> fatosCuriosos = new List<FatoCurioso>();
        private static int lastId = 0;

        public static LocalDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new LocalDatabase();
            }

            return instance;
        }

        public void insert(FatoCurioso fatoCurioso)
        { 
            /*
             Caso o código seja -1, então quer dizer que é um
             registro novo, visto que o código padrão da criação
             da nova instância da classe é Codigo = -1.
             */
            if (fatoCurioso.Codigo == -1)
            {
                lastId++;

                fatoCurioso.Codigo = lastId;
            }
            fatoCurioso.Tags = convertTags(fatoCurioso.Tags);

            fatosCuriosos.Add(fatoCurioso);
        }

        private string convertTags(string tags, bool reverse = false)
        {
            string separator = ",";
            string joiner = "|";

            if (reverse)
            {
                separator = "|";
                joiner = ",";
            }

            string[] tagsList = tags.Split(separator);
            // Remover os espaços em branco antes e depois de cada tag.
            for (int counter = 0; counter < tagsList.Length; counter++)
            {
                tagsList[counter] = tagsList[counter].Trim();
            }
            string tagsFormatadas = String.Join(joiner, tagsList);

            return tagsFormatadas;
        }

        public List<FatoCurioso> list()
        {
            return fatosCuriosos;
        }

        public void update(FatoCurioso fatoCurioso)
        {
            int index = getIndex(fatoCurioso.Codigo);

            if (index != -1)
            {
                fatosCuriosos[index] = fatoCurioso;
            }
            fatoCurioso.Tags = convertTags(fatoCurioso.Tags);

            fatosCuriosos[index] = fatoCurioso;
        }

        public FatoCurioso? get(int codigo)
        {
            FatoCurioso? fatoCurioso = null;

            int selectedIndex = getIndex(codigo);

            if (selectedIndex != -1)
            {
                fatoCurioso = fatosCuriosos[selectedIndex];
                fatoCurioso.Tags = convertTags(fatoCurioso.Tags, true);
            }

            return fatoCurioso;
        }

        public void delete(int codigo)
        {
            int index = getIndex(codigo);

            if (index != -1)
            {
                fatosCuriosos.RemoveAt(index);
            }
        }

        public int getIndex(int codigo)
        {
            int index = -1;

            for (int counter = 0; counter < fatosCuriosos.Count; counter++)
            {
                FatoCurioso fatoCurioso = fatosCuriosos[counter];

                if (fatoCurioso.Codigo == codigo)
                {
                    index = counter;

                    break;
                }
            }

            return index;
        }
    }
}
