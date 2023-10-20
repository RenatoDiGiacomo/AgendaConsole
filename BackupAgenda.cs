﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole
{
    internal class BackupAgenda
    {

        public static string nomeArquivo = "dados.txt";

        public static void SalvarDados(ref string[] nome, ref string[] email, ref int tl)
        {
            StreamWriter sr = new StreamWriter(nomeArquivo);
            for (int i = 0; i < tl; i++)
            {
                sr.WriteLine(nome[i] + "-" + email[i]);
            }
            sr.Close();
        }
        public static void RestaurarDados(ref string[] nome, ref string[] email, ref int tl)
        {

            tl = 0;
            int pos = 0;

            if (File.Exists(nomeArquivo))
            {
                StreamReader sr = new StreamReader(nomeArquivo);
                if (sr != null)
                {
                    Console.WriteLine(sr);
                }
                string line = sr.ReadLine();
                while (line != null)
                {
                    pos = line.IndexOf("-");

                    nome[tl] = line.Substring(0, pos);
                    email[tl] = line.Substring(pos + 1);
                    tl++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }

            
        }
    }
}
