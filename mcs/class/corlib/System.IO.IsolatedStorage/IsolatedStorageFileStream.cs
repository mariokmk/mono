//
// System.IO.IsolatedStorage.IsolatedStorageFileStream
//
// Authors
//	Sean MacIsaac (macisaac@ximian.com)
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2001 Ximian, Inc.
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Globalization;
using System.Reflection;

#if NET_2_0
using Microsoft.Win32.SafeHandles;
#endif

namespace System.IO.IsolatedStorage {

	public class IsolatedStorageFileStream : FileStream {

		private static string CreateIsolatedPath (IsolatedStorageFile isf, string path)
		{
			if (path == null)
				throw new ArgumentNullException ("path");

			if (isf == null)
				isf = IsolatedStorageFile.GetUserStoreForDomain (); 

			string file = Path.Combine (isf.Root, path);

			// Ensure that the file can be created.
			FileInfo fi = new FileInfo (file);
			if (!fi.Directory.Exists)
				fi.Directory.Create ();

			// FIXME: this is probably a good place to Assert our security
			// needs (once Mono supports imperative security stack modifiers)

			return file;
		}

		public IsolatedStorageFileStream (string path, FileMode mode)
			: base (CreateIsolatedPath (null, path), mode, (mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite), FileShare.Read, DefaultBufferSize, false, true)
		{
		}	

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access)
			: base (CreateIsolatedPath (null, path), mode, access, access == FileAccess.Write ? FileShare.None : FileShare.Read, DefaultBufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access, FileShare share)
			: base (CreateIsolatedPath (null, path), mode, access, share, DefaultBufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: base (CreateIsolatedPath (null, path), mode, access, share, bufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
			: base (CreateIsolatedPath (isf, path), mode, access, share, bufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
			: base (CreateIsolatedPath (isf, path), mode, access, share, DefaultBufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
			: base (CreateIsolatedPath (isf, path), mode, access, access == FileAccess.Write ? FileShare.None : FileShare.Read, DefaultBufferSize, false, true)
		{
		}

		public IsolatedStorageFileStream (string path, FileMode mode, IsolatedStorageFile isf)
			: base (CreateIsolatedPath (isf, path), mode, (mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite), FileShare.Read, DefaultBufferSize, false, true)
		{
		}

		public override bool CanRead {
			get {return base.CanRead;}
		}

		public override bool CanSeek {
			get {return base.CanSeek;}
		}

		public override bool CanWrite {
			get {return base.CanWrite;}
		}

#if NET_2_0
		public override SafeFileHandle SafeFileHandle {
			get {
				throw new IsolatedStorageException (
					Locale.GetText ("Information is restricted"));
			}
		}

		[Obsolete ("Use SafeFileHandle - once available")]
#endif
		public override IntPtr Handle {
			get {
				throw new IsolatedStorageException (
					Locale.GetText ("Information is restricted"));
			}
		}

		public override bool IsAsync {
			get {return base.IsAsync;}
		}

		public override long Length {
			get {return base.Length;}
		}

		public override long Position {
			get {return base.Position;}
			set {base.Position = value;}
		}

		public override IAsyncResult BeginRead (byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return base.BeginRead (buffer, offset, numBytes, userCallback, stateObject);
		}

		public override IAsyncResult BeginWrite (byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return base.BeginWrite (buffer, offset, numBytes, userCallback, stateObject);
		}

		public override void Close ()
		{
			base.Close ();
		}

		public override int EndRead (IAsyncResult asyncResult)
		{
			return base.EndRead (asyncResult);
		}

		public override void EndWrite (IAsyncResult asyncResult)
		{
			base.EndWrite (asyncResult);
		}

		public override void Flush ()
		{
			base.Flush ();
		}

		public override int Read (byte[] buffer, int offset, int count)
		{
			return base.Read (buffer, offset, count);
		}

		public override int ReadByte ()
		{
			return base.ReadByte ();
		}

		public override long Seek (long offset, SeekOrigin origin)
		{
			return base.Seek (offset, origin);
		}

		public override void SetLength (long value)
		{
			base.SetLength (value);
		}

		public override void Write (byte[] buffer, int offset, int count)
		{
			base.Write (buffer, offset, count);
		}

		public override void WriteByte (byte value)
		{
			base.WriteByte (value);
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}
	}
}
