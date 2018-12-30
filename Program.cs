using CSCore;
using CSCore.SoundOut;
using System.IO;
using System.IO.Compression;

namespace scap
{
    class Program
    {
        static void Play(Stream stream)
        {
            using (IWaveSource soundSource = new CSCore.Codecs.AAC.AacDecoder(stream))
            using (ISoundOut soundOut = new DirectSoundOut())
            {
                soundOut.Initialize(soundSource);
                soundOut.Play();
                while (soundOut.PlaybackState == PlaybackState.Playing) ;
            }
        }

        static void Main(string[] args)
        {
            using (var ms = new MemoryStream())
            using (var strm = new MemoryStream(Properties.Resources.sound))
            using (var st = new DeflateStream(strm, CompressionMode.Decompress))
            {
                st.CopyTo(ms);
                Play(ms);
            }
        }
    }
}
