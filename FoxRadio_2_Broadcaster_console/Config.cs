﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxRadio_2_Broadcaster_console
{
	static class Config
	{
		private const string CONFIG_FILE_DIR = "CONFIG.cfg";
		public enum ConfigType
		{
			String,
			Int,
			Boolean
		}
		public static Hashtable ConfigData = new Hashtable( );

		public static bool Load( )
		{
			if ( System.IO.File.Exists( CONFIG_FILE_DIR ) )
			{
				foreach ( string i in System.IO.File.ReadAllLines( CONFIG_FILE_DIR ) )
				{
					if ( string.IsNullOrEmpty( i ) ) continue;
					if ( i.StartsWith( "[" ) && i.EndsWith( "]" ) ) continue;

					string[ ] c = i.Split( new char[ ] { '=' }, StringSplitOptions.RemoveEmptyEntries );

					if ( c.Length == 2 )
					{
						ConfigData[ c[ 0 ].Trim( ) ] = c[ 1 ].Trim( );
						Console.WriteLine( "CONFIG Register : {0} -> {1}", c[ 0 ].Trim( ), c[ 1 ].Trim( ) );
					}
				}

				return true;
			}
			else
				return false;
		}

		public static T GetData<T>( string ID, Action<string> ErrorCallBack = null, T Default = default( T ) )
		{
			object Data = ConfigData[ ID ];

			if ( Data != null )
				return ( T ) Data;
			else
			{
				ErrorCallBack?.DynamicInvoke( ID );
				return Default;
			}
		}
	}
}
