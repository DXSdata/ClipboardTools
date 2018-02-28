using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ClipboardTools
{
    /// <summary>
    /// https://stackoverflow.com/questions/24985239/dropped-zip-file-causes-e-data-getdatafilecontents-to-throw-an-exception
    /// </summary>
    static class VirtualFileHelper
    {
        public static MemoryStream GetFileContents(System.Windows.IDataObject dataObject, int index)
        {
            IDataObject comDataObject;
            comDataObject = (IDataObject)dataObject;

            System.Windows.DataFormat Format = System.Windows.DataFormats.GetDataFormat("FileContents");
            if (Format == null)
                return null;

            FORMATETC formatetc = new FORMATETC();
            formatetc.cfFormat = (short)Format.Id;
            formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
            formatetc.lindex = index;
            formatetc.tymed = TYMED.TYMED_ISTREAM | TYMED.TYMED_HGLOBAL;


            //create STGMEDIUM to output request results into
            STGMEDIUM medium = new STGMEDIUM();

            //using the com IDataObject interface get the data using the defined FORMATETC
            comDataObject.GetData(ref formatetc, out medium);

            switch (medium.tymed)
            {
                case TYMED.TYMED_ISTREAM: return GetIStream(medium);
                default: throw new NotSupportedException();
            }
        }

        private static MemoryStream GetIStream(STGMEDIUM medium)
        {
            //marshal the returned pointer to a IStream object
            IStream iStream = (IStream)Marshal.GetObjectForIUnknown(medium.unionmember);
            Marshal.Release(medium.unionmember);

            //get the STATSTG of the IStream to determine how many bytes are in it
            var iStreamStat = new System.Runtime.InteropServices.ComTypes.STATSTG();
            iStream.Stat(out iStreamStat, 0);
            int iStreamSize = (int)iStreamStat.cbSize;

            //prevent memoryexception
            if (iStreamSize > 100000000)
                return null;

            //read the data from the IStream into a managed byte array
            byte[] iStreamContent = new byte[iStreamSize];
            iStream.Read(iStreamContent, iStreamContent.Length, IntPtr.Zero);

            //wrapped the managed byte array into a memory stream
            return new MemoryStream(iStreamContent);
        }
    }
}
