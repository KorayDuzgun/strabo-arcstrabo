﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Strabo.Core.ImageProcessing;

namespace Strabo.Core.TextDetection
{
    public class MergeTextStrings
    {
        public List<TextString> text_string_list = new List<TextString>();

        public MergeTextStrings() { }
        public void AddTextString(TextString dstString)
        {
            Update(ref dstString);
            List<int> insert_idx_list = new List<int>();// insert = -1;
            int[] matched_idx_array = new int[dstString.char_list.Count];
            for(int i=0;i<matched_idx_array.Length;i++)
                matched_idx_array[i] = -1;
            int[] matched_char_blob_count = new int [text_string_list.Count];
            for (int i = 0; i < text_string_list.Count; i++)
            {
                TextString srcString = text_string_list[i];
                for(int x=0;x<srcString.char_list.Count;x++)
                {
                    for(int y=0;y<dstString.char_list.Count;y++)
                    {
                        if(Distance(srcString.char_list[x].mass_center,dstString.char_list[y].mass_center)<3)
                        {
                            matched_idx_array[y] = i;
                            matched_char_blob_count[i]++;
                        }
                    }
                      //if(insert!=-1)
                            //break;
                }
            }
            int max_matched = 0;
            int max_matched_idx = -1;
            for (int i = 0; i < matched_char_blob_count.Length; i++)
            {
                if (matched_char_blob_count[i] > max_matched)
                {
                    max_matched = matched_char_blob_count[i];
                    max_matched_idx = i;
                }
            }
            int insert = max_matched_idx;
            if (insert == -1) text_string_list.Add(dstString);
            else
            {
                List<int> remove_string_list = new List<int>();
                for (int i = 0; i < matched_idx_array.Length ; i++)
                {
                    if (matched_idx_array[i] != -1 && matched_idx_array[i] != insert)
                        remove_string_list.Add(i);
                }
                Point p1  =  new Point(text_string_list[insert].bbx.X,text_string_list[insert].bbx.Y);
                Point p2 = new Point(dstString.bbx.X, dstString.bbx.Y);
                for (int c = 0; c < dstString.char_list.Count; c++)
                    text_string_list[insert].AddChar(dstString.char_list[c]);

                Bitmap newimg1 = new Bitmap(text_string_list[insert].bbx.Width, text_string_list[insert].bbx.Height);
                Bitmap newimg2 = new Bitmap(text_string_list[insert].bbx.Width, text_string_list[insert].bbx.Height);
                Graphics g1 = Graphics.FromImage(newimg1);
                g1.Clear(Color.White);
                g1.DrawImage(text_string_list[insert].srcimg,
                    p1.X - text_string_list[insert].bbx.X,
                    p1.Y - text_string_list[insert].bbx.Y);

                Graphics g2 = Graphics.FromImage(newimg2);
                g2.Clear(Color.White);
                g2.DrawImage(dstString.srcimg,
                    p2.X - text_string_list[insert].bbx.X,
                    p2.Y - text_string_list[insert].bbx.Y);
                g1.Dispose(); g2.Dispose();
                
                //ASHISH
                newimg1 = ImageUtils.GetIntersection(newimg1, newimg2);

                text_string_list[insert].srcimg = newimg1;

                //for (int i = 0; i < remove_string_list.Count; i++)
                //{

                //}
            }
        }
        
        public double Distance(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
        public void Update(ref TextString srcString)
        {
            MyConnectedComponentsAnalysisFast.MyBlob char_blob0 = srcString.char_list[0];
            char_blob0.mass_center.X += srcString.x_offset;
            char_blob0.mass_center.Y += srcString.y_offset;
            char_blob0.bbx.X += srcString.x_offset;
            char_blob0.bbx.Y += srcString.y_offset;
            Rectangle bbx = char_blob0.bbx;

            for (int i = 1; i < srcString.char_list.Count; i++)
            {
                MyConnectedComponentsAnalysisFast.MyBlob char_blob = srcString.char_list[i];
                char_blob.mass_center.X += srcString.x_offset;
                char_blob.mass_center.Y += srcString.y_offset;
                char_blob.bbx.X += srcString.x_offset;
                char_blob.bbx.Y += srcString.y_offset;

                int x = bbx.X, y = bbx.Y, xx = bbx.X + bbx.Width - 1, yy = bbx.Y + bbx.Height - 1;
                int x1 = char_blob.bbx.X, y1 = char_blob.bbx.Y, xx1 = char_blob.bbx.X + char_blob.bbx.Width - 1, yy1 = char_blob.bbx.Y + char_blob.bbx.Height - 1;

                int x2, y2, xx2, yy2;

                if (x < x1) x2 = x;
                else x2 = x1;
                if (y < y1) y2 = y;
                else y2 = y1;

                if (xx < xx1) xx2 = xx1;
                else xx2 = xx;
                if (yy < yy1) yy2 = yy1;
                else yy2 = yy;

                bbx.X = x2; bbx.Y = y2;
                bbx.Width = xx2 - x2 + 1;
                bbx.Height = yy2 - y2 + 1;
            }
            srcString.bbx = bbx;
        }
    }
}
