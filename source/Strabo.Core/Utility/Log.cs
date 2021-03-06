﻿/*******************************************************************************
 * Copyright 2010 University of Southern California
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * 	http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * This code was developed as part of the Strabo map processing project 
 * by the Spatial Sciences Institute and by the Information Integration Group 
 * at the Information Sciences Institute of the University of Southern 
 * California. For more information, publications, and related projects, 
 * please see: http://yoyoi.info and http://www.isi.edu/integration
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Strabo.Core.Utility
{
    public class Log
    {
        static public bool console = false;
        static public int image_counter =0;
        static public DateTime start;
        static public DateTime[] start_timer = new DateTime[10]; 
        static private string output_dir="";
        static private string debug_dir = "";
        static private string log_dir = "";
        public Log() { }
        static public void SetOutputDir(string dir)
        {
            output_dir = dir;
        }
        static public void SetDebugDir(string dir)
        {
            debug_dir = dir;
        }
        static public void SetLogDir(string dir)
        {
            log_dir = dir;
        }
        static public void SetStartTime()
        {
            start = DateTime.Now;
        }
        static public void SetStartTime(int i)
        {
            start_timer[i] = DateTime.Now;
        }
        static public double GetDurationInSeconds()
        {
            DateTime stop = DateTime.Now;
            TimeSpan elapsedTime = stop - start;
            return elapsedTime.TotalSeconds;
        }
        static public double GetDurationInSeconds(int i)
        {
            DateTime stop = DateTime.Now;
            TimeSpan elapsedTime = stop - start_timer[i];
            return elapsedTime.TotalSeconds;
        }
        static public void ArchiveLog()
        {
            DateTime now = DateTime.Now;
            string date = now.ToString(); 
            date = date.Replace('/', '-'); date = date.Replace(':', '-');
            if (File.Exists(output_dir + "log.txt"))
                File.Move(output_dir + "log.txt", log_dir + "log" + date + ".txt");
        }
        static public void DeleteAll()
        {
            DirectoryInfo TheFolder = new DirectoryInfo(log_dir);
            if (TheFolder.GetFiles() != null)
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                    File.Delete(NextFile.FullName);
            TheFolder = new DirectoryInfo(debug_dir);
            if (TheFolder.GetFiles() != null)
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                    File.Delete(NextFile.FullName);
            TheFolder = new DirectoryInfo(output_dir);
            if (TheFolder.GetFiles() != null)
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                    File.Delete(NextFile.FullName);
        }
        static public void DeleteAll(string dir)
        {
            try
            {
                DirectoryInfo TheFolder = new DirectoryInfo(dir);
                if (TheFolder.GetFiles() != null)
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                        File.Delete(NextFile.FullName);
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        static public void DeleteAllDebug()
        {
            try
            {
                DirectoryInfo TheFolder = new DirectoryInfo(debug_dir);
                if (TheFolder.GetFiles() != null)
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                        File.Delete(NextFile.FullName);
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        static public void DeleteAllOutput()
        {
            try
            {
                DirectoryInfo TheFolder = new DirectoryInfo(output_dir);
                if (TheFolder.GetFiles() != null)
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                        File.Delete(NextFile.FullName);
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        static public void Write(string input)
        {
            if (console)
                Console.Write(input);
            else
            {
                StreamWriter sw = new StreamWriter(log_dir + "log.txt", true);
                sw.Write(input);
                sw.Close();
            }
        }
        static public void WriteLine(string input)
        {
            if (console)
                Console.WriteLine(input);
            else
            {
                StreamWriter sw = new StreamWriter(log_dir + "\\" + "log.txt", true);
                //sw.WriteLine("                      "+DateTime.Now);
                sw.WriteLine(DateTime.Now+" "+input);
                sw.Close();
            }
        }
        static public void WriteBitmap2Debug(Bitmap srcimg, string fn)
        {
            try
            {
                //string test = "skip write";
                srcimg.Save(debug_dir + image_counter.ToString("0000")+Path.GetFileNameWithoutExtension(fn) + ".png", ImageFormat.Png);
                image_counter++;
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        static public void WriteBitmap2DebugExactFileName(Bitmap srcimg, string fn)
        {
            try
            {
                //string test = "skip write";
                srcimg.Save(debug_dir + Path.GetFileNameWithoutExtension(fn) + ".png", ImageFormat.Png);
                image_counter++;
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        static public void WriteBitmap2FolderExactFileName(string folder,Bitmap srcimg, string fn)
        {
            try
            {
                //string test = "skip write";
                //Console.WriteLine(fn);
                srcimg.Save(folder + Path.GetFileNameWithoutExtension(fn) + ".png", ImageFormat.Png);
                image_counter++;
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
        //static public void WriteBitmap(Bitmap srcimg, string fn)
        //{
        //    WriteBitmap2Debug(srcimg, fn);
        //}
        static public void WriteBitmap2Output(Bitmap srcimg, string fn)
        {
            try
            {
                srcimg.Save(output_dir +fn + ".png", ImageFormat.Png);
                image_counter++;
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
                Log.WriteLine(e.ToString());
            }
        }
    }
}
