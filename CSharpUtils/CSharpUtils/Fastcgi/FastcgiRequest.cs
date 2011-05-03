﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;

namespace CSharpUtils.Fastcgi
{
	public class FastcgiRequest
	{
		public ushort RequestId;
		public FastcgiHandler FastcgiHandler;
		public Stream ParamsStream;
		public Stream StdinStream;
		public FascgiResponseOutputStream StdoutStream;
		public FascgiResponseOutputStream StderrStream;

		public FastcgiRequest(FastcgiHandler FastcgiHandler, ushort RequestId)
		{
			this.FastcgiHandler = FastcgiHandler;
			this.RequestId = RequestId;
			this.ParamsStream = new MemoryStream();
			this.StdinStream = new MemoryStream();
			this.StdoutStream = new FascgiResponseOutputStream(this, Fastcgi.PacketType.FCGI_STDOUT);
			this.StderrStream = new FascgiResponseOutputStream(this, Fastcgi.PacketType.FCGI_STDERR);
		}

		public void ParseParamsStream()
		{
			Params = new Dictionary<string, string>();
			ParamsStream.Position = 0;
			while (!ParamsStream.Eof())
			{
				var Key = new byte[FastcgiPacketReader.ReadVariableInt(ParamsStream)];
				var Value = new byte[FastcgiPacketReader.ReadVariableInt(ParamsStream)];
				ParamsStream.Read(Key, 0, Key.Length);
				ParamsStream.Read(Value, 0, Value.Length);
				Params[Encoding.ASCII.GetString(Key)] = Encoding.ASCII.GetString(Value);
			}
			ParamsStream = new MemoryStream();
		}

		public Dictionary<String, String> Params = new Dictionary<string, string>();
	}
}
