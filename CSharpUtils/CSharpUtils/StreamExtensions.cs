﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace CSharpUtils
{
	static public class StreamExtensions
	{
		static public String ReadAllContentsAsString(this Stream Stream, Encoding Encoding = null)
		{
			if (Encoding == null) Encoding = Encoding.UTF8;
			return Encoding.GetString(Stream.ReadAll());
		}

		static public byte[] ReadAll(this Stream Stream)
		{
            lock (Stream)
            {
                var OldPosition = Stream.Position;
                var Data = new byte[Stream.Length];
                Stream.Position = 0;
                Stream.Read(Data, 0, Data.Length);
                Stream.Position = OldPosition;
                return Data;
            }
		}

        static public byte[] ReadBytes(this Stream Stream, int ToRead)
        {
            var Buffer = new byte[ToRead];
            var Readed = Stream.Read(Buffer, 0, ToRead);
            if (Readed != ToRead) throw(new Exception("Unable to read " + ToRead + " bytes, readed " + Readed + "."));
            return Buffer;
        }

        static public void WriteBytes(this Stream Stream, byte[] Bytes)
        {
            Stream.Write(Bytes, 0, Bytes.Length);
        }

        static public String ReadStringz(this Stream Stream, int ToRead = -1, Encoding Encoding = null)
        {
            if (Encoding == null) Encoding = Encoding.ASCII;
            if (ToRead == -1)
            {
                var Temp = new MemoryStream();
                byte Byte;
                while ((Byte = (byte)Stream.ReadByte()) != 0)
                {
                    Temp.WriteByte(Byte);
                }
                return Encoding.GetString(Temp.ToArray());
            }
            else
            {
                return Encoding.GetString(Stream.ReadBytes(ToRead)).TrimEnd('\0');
            }
        }

        static public void WriteStringz(this Stream Stream, String Value, int ToWrite = -1, Encoding Encoding = null)
        {
            if (Encoding == null) Encoding = Encoding.ASCII;
            if (ToWrite == -1)
            {
                Stream.WriteBytes(Value.GetStringzBytes(Encoding));
            }
            else
            {
                byte[] Bytes = Encoding.GetBytes(Value);
                if (Bytes.Length > ToWrite) throw(new Exception("String too long"));
                Stream.WriteBytes(Bytes);
                ToWrite -= Bytes.Length;
                while (ToWrite-- > 0)
                {
                    Stream.WriteByte(0);
                }
            }
        }

        static public void WriteZeroToPadding(this Stream Stream, int Padding)
        {
            while ((Stream.Position % Padding) != 0)
            {
                Stream.WriteByte(0);
            }
        }

        static public void WriteZeroToOffset(this Stream Stream, long Offset)
        {
            while (Stream.Position < Offset)
            {
                Stream.WriteByte(0);
            }
        }

        public static T ReadStruct<T>(this Stream Stream) where T : struct
        {
            var Size = Marshal.SizeOf(typeof(T));
            var Buffer = new byte[Size];
            Stream.Read(Buffer, 0, Size);
            return StructUtils.BytesToStruct<T>(Buffer);
        }

        public static void WriteStruct<T>(this Stream Stream, T Struct) where T : struct
        {
            byte[] Bytes = StructUtils.StructToBytes(Struct);
            Stream.Write(Bytes, 0, Bytes.Length);
        }

        public static void CopyToFile(this Stream Stream, String FileName)
        {
            using (var OutputFile = File.OpenWrite(FileName))
            {
                Stream.CopyTo(OutputFile);
            }
        }

        public static Stream SetPosition(this Stream Stream, long Position)
        {
            Stream.Position = Position;
            return Stream;
        }
	}
}
