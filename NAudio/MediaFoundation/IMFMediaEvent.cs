﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
    /// <summary>
    /// IMFMediaEvent - Represents an event generated by a Media Foundation object. Use this interface to get information about the event.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms702249%28v=vs.85%29.aspx
    /// Mfobjects.h
    /// </summary>
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3")]
    public interface IMFMediaEvent : IMFAttributes
    {
        /// <summary>
        /// Retrieves the value associated with a key.
        /// </summary>
        new void GetItem([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, Out] IntPtr pValue);

        /// <summary>
        /// Retrieves the data type of the value associated with a key.
        /// </summary>
        new void GetItemType([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pType);

        /// <summary>
        /// Queries whether a stored attribute value equals a specified PROPVARIANT.
        /// </summary>
        new void CompareItem([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

        /// <summary>
        /// Compares the attributes on this object with the attributes on another object.
        /// </summary>
        new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int matchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

        /// <summary>
        /// Retrieves a UINT32 value associated with a key.
        /// </summary>
        new void GetUINT32([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int punValue);

        /// <summary>
        /// Retrieves a UINT64 value associated with a key.
        /// </summary>
        new void GetUINT64([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out long punValue);

        /// <summary>
        /// Retrieves a double value associated with a key.
        /// </summary>
        new void GetDouble([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out double pfValue);

        /// <summary>
        /// Retrieves a GUID value associated with a key.
        /// </summary>
        new void GetGUID([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out Guid pguidValue);

        /// <summary>
        /// Retrieves the length of a string value associated with a key.
        /// </summary>
        new void GetStringLength([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcchLength);

        /// <summary>
        /// Retrieves a wide-character string associated with a key.
        /// </summary>
        new void GetString([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszValue, int cchBufSize,
                       out int pcchLength);

        /// <summary>
        /// Retrieves a wide-character string associated with a key. This method allocates the memory for the string.
        /// </summary>
        new void GetAllocatedString([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue,
                                out int pcchLength);

        /// <summary>
        /// Retrieves the length of a byte array associated with a key.
        /// </summary>
        new void GetBlobSize([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out int pcbBlobSize);

        /// <summary>
        /// Retrieves a byte array associated with a key.
        /// </summary>
        new void GetBlob([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pBuf, int cbBufSize,
                     out int pcbBlobSize);

        /// <summary>
        /// Retrieves a byte array associated with a key. This method allocates the memory for the array.
        /// </summary>
        new void GetAllocatedBlob([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, out IntPtr ip, out int pcbSize);

        /// <summary>
        /// Retrieves an interface pointer associated with a key.
        /// </summary>
        new void GetUnknown([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                        [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        /// <summary>
        /// Associates an attribute value with a key.
        /// </summary>
        new void SetItem([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, IntPtr value);

        /// <summary>
        /// Removes a key/value pair from the object's attribute list.
        /// </summary>
        new void DeleteItem([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey);

        /// <summary>
        /// Removes all key/value pairs from the object's attribute list.
        /// </summary>
        new void DeleteAllItems();

        /// <summary>
        /// Associates a UINT32 value with a key.
        /// </summary>
        new void SetUINT32([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, int unValue);

        /// <summary>
        /// Associates a UINT64 value with a key.
        /// </summary>
        new void SetUINT64([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, long unValue);

        /// <summary>
        /// Associates a double value with a key.
        /// </summary>
        new void SetDouble([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, double fValue);

        /// <summary>
        /// Associates a GUID value with a key.
        /// </summary>
        new void SetGUID([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidValue);

        /// <summary>
        /// Associates a wide-character string with a key.
        /// </summary>
        new void SetString([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, MarshalAs(UnmanagedType.LPWStr)] string wszValue);

        /// <summary>
        /// Associates a byte array with a key.
        /// </summary>
        new void SetBlob([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pBuf,
                     int cbBufSize);

        /// <summary>
        /// Associates an IUnknown pointer with a key.
        /// </summary>
        new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown);

        /// <summary>
        /// Locks the attribute store so that no other thread can access it.
        /// </summary>
        new void LockStore();

        /// <summary>
        /// Unlocks the attribute store.
        /// </summary>
        new void UnlockStore();

        /// <summary>
        /// Retrieves the number of attributes that are set on this object.
        /// </summary>
        new void GetCount(out int pcItems);

        /// <summary>
        /// Retrieves an attribute at the specified index.
        /// </summary>
        new void GetItemByIndex(int unIndex, out Guid pGuidKey, [In, Out] IntPtr pValue);

        /// <summary>
        /// Copies all of the attributes from this object into another attribute store.
        /// </summary>
        new void CopyAllItems([In, MarshalAs(UnmanagedType.Interface)] IMFAttributes pDest);
        
        /// <summary>
        /// Retrieves the event type. 
        /// </summary>
        /// <remarks>
        /// virtual HRESULT STDMETHODCALLTYPE GetType( 
        ///     /* [out] */ __RPC__out MediaEventType *pmet) = 0;
        /// </remarks>
        void GetType([Out] out MediaEventType pmet);

        /// <summary>
        /// Retrieves the extended type of the event.
        /// </summary>
        /// <remarks>
        /// virtual HRESULT STDMETHODCALLTYPE GetExtendedType( 
        ///     /* [out] */ __RPC__out GUID *pguidExtendedType) = 0;
        /// </remarks>
        void GetExtendedType([Out] out Guid pguidExtendedType);

        /// <summary>
        /// Retrieves an HRESULT that specifies the event status.
        /// </summary>
        /// <remarks>
        /// virtual HRESULT STDMETHODCALLTYPE GetStatus( 
        ///     /* [out] */ __RPC__out HRESULT *phrStatus) = 0;
        /// </remarks>
        void GetStatus([MarshalAs(UnmanagedType.Error)] out int phrStatus);

        /// <summary>
        /// Retrieves the value associated with the event, if any. 
        /// </summary>
        /// <remarks>
        /// virtual HRESULT STDMETHODCALLTYPE GetValue( 
        ///     /* [out] */ __RPC__out PROPVARIANT *pvValue) = 0;
        /// </remarks>
        void GetValue([Out]IntPtr pvValue);
    }
}
