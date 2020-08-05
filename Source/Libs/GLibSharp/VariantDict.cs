// Copyright (c) 2011 Novell, Inc.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2 of the Lesser GNU General 
// Public License as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this program; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GLib {

	public class VariantDict : IDisposable {

		IntPtr handle;
		public IntPtr Handle {
			get { return handle; }
		}

		~VariantDict ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void d_g_variant_dict_unref(IntPtr dict);
		static d_g_variant_dict_unref g_variant_unref = FuncLoader.LoadFunction<d_g_variant_dict_unref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_unref"));

		void Dispose (bool disposing)
		{
			if (handle == IntPtr.Zero)
				return;

			g_variant_unref (handle);
			handle = IntPtr.Zero;
			if (disposing)
				GC.SuppressFinalize (this);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr d_g_variant_dict_ref(IntPtr dict);
		static d_g_variant_dict_ref g_variant_dict_ref = FuncLoader.LoadFunction<d_g_variant_dict_ref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_ref"));

		public VariantDict(IntPtr handle)
		{
			this.handle = g_variant_dict_ref(handle);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr d_g_variant_dict_new(IntPtr from_asv);
		static d_g_variant_dict_new g_variant_dict_new = FuncLoader.LoadFunction<d_g_variant_dict_new>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_new"));

		public VariantDict (Variant from_asv)
		{
			this.handle = g_variant_dict_new (from_asv.Handle);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate bool d_g_variant_dict_contains(IntPtr dict, IntPtr key);
		static d_g_variant_dict_contains g_variant_dict_contains = FuncLoader.LoadFunction<d_g_variant_dict_contains>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_contains"));

		public bool Contains(string key)
		{
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup(key);
			bool result = g_variant_dict_contains(Handle, native_key);
			GLib.Marshaller.Free(native_key);
			return result;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr d_g_variant_dict_lookup_value(IntPtr dict, IntPtr key, IntPtr expected_type);
		static d_g_variant_dict_lookup_value g_variant_dict_lookup_value = FuncLoader.LoadFunction<d_g_variant_dict_lookup_value>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_lookup_value"));

		public Variant LookupValue(string key, VariantType expected_type)
		{
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup(key);
			IntPtr result = g_variant_dict_lookup_value(Handle, native_key, expected_type.Handle);
			GLib.Marshaller.Free(native_key);

			return result != IntPtr.Zero ? new Variant(result) : null;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate bool d_g_variant_dict_insert_value(IntPtr dict, IntPtr key, IntPtr value);
		static d_g_variant_dict_insert_value g_variant_dict_insert_value = FuncLoader.LoadFunction<d_g_variant_dict_insert_value>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_insert_value"));

		public void InsertValue(string key, Variant value)
		{
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup(key);
			g_variant_dict_insert_value(Handle, native_key, value.Handle);
			GLib.Marshaller.Free(native_key);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate bool d_g_variant_dict_remove(IntPtr dict, IntPtr key);
		static d_g_variant_dict_remove g_variant_dict_remove = FuncLoader.LoadFunction<d_g_variant_dict_remove>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_remove"));

		public bool Remove(string key)
		{
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup(key);
			bool result = g_variant_dict_remove(Handle, native_key);
			GLib.Marshaller.Free(native_key);

			return result;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate IntPtr d_g_variant_dict_end(IntPtr dict);
		static d_g_variant_dict_end g_variant_dict_end = FuncLoader.LoadFunction<d_g_variant_dict_end>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_variant_dict_end"));

		public Variant End()
		{
			return new Variant(g_variant_dict_end(Handle));
		}
	}
}

