using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace CogEngine.Objects.XNA
{
    public class SomXNA
    {
        private Som _Som;
        private SoundEffect _Sound;

        public SomXNA(Som som)
        {
            _Som = som;
            using (FileStream f = File.Open(_Som.CaminhoCompleto, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _Sound = SoundEffect.FromStream(f);
            }
        }

        public string Nome
        {
            get
            {
                return _Som.NomeArquivo;
            }
        }

        public void Tocar()
        {
            _Sound.Play();
        }
    }
}
