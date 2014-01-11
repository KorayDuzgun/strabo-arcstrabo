﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Strabo.Core.ImageProcessing;

namespace Strabo.Core.TextDetection
{
    public class DetectTextStrings
    {
        int width, height;
        int min_width = 10, min_height = 10;
        int max_width = 500, max_height = 500;
        public DetectTextStrings() { }
       
        public List<TextString> Apply(Bitmap srcimg, Bitmap dilatedimg)
        {
            width = srcimg.Width;
            height = srcimg.Height;
            max_width = width / 2;
            max_height = height / 2;
            //ashish
            srcimg = ImageUtils.ConvertGrayScaleToBinary(srcimg, threshold: 128);
            srcimg = ImageUtils.InvertColors(srcimg);
            dilatedimg = ImageUtils.ConvertGrayScaleToBinary(dilatedimg, threshold: 128);
            dilatedimg = ImageUtils.InvertColors(dilatedimg);

            MyConnectedComponentsAnalysisFast.MyBlobCounter char_bc = new MyConnectedComponentsAnalysisFast.MyBlobCounter();
            List<MyConnectedComponentsAnalysisFast.MyBlob> char_blobs = char_bc.GetBlobs(srcimg);
            ushort[] char_labels = char_bc.objectLabels;
            
            MyConnectedComponentsAnalysisFast.MyBlobCounter string_bc = new MyConnectedComponentsAnalysisFast.MyBlobCounter();
            List<MyConnectedComponentsAnalysisFast.MyBlob> string_blobs = string_bc.GetBlobs(dilatedimg);
            ushort[] string_labels = string_bc.objectLabels;

            List<TextString> initial_string_list = new List<TextString>();

            int string_count = string_blobs.Count;
            for (int i = 0; i < string_count; i++)
            {
                initial_string_list.Add(new TextString());
                initial_string_list.Last().mass_center = string_blobs[i].mass_center;
            }

            for (int i = 0; i < char_blobs.Count; i++)
            {
                if (char_blobs[i].bbx.Width > 1 && char_blobs[i].bbx.Height > 1)
                {
                    char_blobs[i].string_id = string_labels[char_blobs[i].sample_y * width + char_blobs[i].sample_x] - 1;
                    initial_string_list[char_blobs[i].string_id].AddChar(char_blobs[i]);
                }
            }
            for (int i = 0; i < initial_string_list.Count; i++)
            {
                if( (initial_string_list[i].char_list.Count == 0) ||
                (initial_string_list[i].bbx.Width<min_width || initial_string_list[i].bbx.Height < min_height) ||
                     (initial_string_list[i].bbx.Width > max_width || initial_string_list[i].bbx.Height > max_height))
                {
                    initial_string_list.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < initial_string_list.Count; i++)
            {
                PrintSubStringsSmall(char_labels, initial_string_list[i], 0);
            }
            return initial_string_list;
        }
        public void PrintSubStringsSmall(ushort[] char_labels, TextString ts, int margin)
        {
            bool[,] stringimg = new bool[ts.bbx.Height + margin, ts.bbx.Width + margin];
            for (int i = 0; i < ts.char_list.Count; i++)
            {
                for (int xx = ts.bbx.X; xx < ts.bbx.X + ts.bbx.Width; xx++)
                    for (int yy = ts.bbx.Y; yy < ts.bbx.Y + ts.bbx.Height; yy++)
                    {
                        if (char_labels[yy * width + xx] == ts.char_list[i].pixel_id)
                            stringimg[yy - ts.bbx.Y + margin / 2, xx - ts.bbx.X + margin / 2] = true;
                    }

            }
            if(ts.char_list.Count >0 )
            ts.srcimg = ImageUtils.ArrayBool2DToBitmap(stringimg);
         
        }
    }
}
