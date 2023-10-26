using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman3
{
     class Ogrenci
    {
        public int Yas { get; set; }
        public string Cinsiyet { get; set; }

        public string Okul { get; set; }

        public int Not { get; set; }     

        public void DersCalis()
        {
            Not = Not + 1;
        }

        public bool CaliskanMi()
        {
            if (Not>80)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
