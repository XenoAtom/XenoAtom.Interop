# XenoAtom.Interop.libdrm [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libdrm.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libdrm.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.libdrm.svg)](https://www.nuget.org/packages/XenoAtom.Interop.libdrm/)

This package provides a low-level and modern .NET P/Invoke wrapper around the libdrm API.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.

libdrm is a userspace library that provides a user-space API to the Direct Rendering Manager. For more information, see [libdrm](https://gitlab.freedesktop.org/mesa/drm) website.
## 💻 Usage

After installing the package, you can access the library through the static class `XenoAtom.Interop.libdrm`.

For more information, see the official documentation at https://gitlab.freedesktop.org/mesa/drm.

## 📦 Compatible Native Binaries

This library does not provide C native binaries but only P/Invoke .NET bindings to `libdrm` `2.4.120-r0`.

If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.
Other OS might require a different setup.


## 📚 Supported API

> This package is based on the following header version:
> 
> - libdrm C include headers: [`libdrm-dev`](https://pkgs.alpinelinux.org/package/v3.20/main/x86_64/libdrm-dev)
> - Version: `2.4.120-r0`
> - Distribution: AlpineLinux `v3.20`

The following API were automatically generated from the C/C++ code:

- xf86drm.h: `drmAddBufs`, `drmAddContextPrivateMapping`, `drmAddContextTag`, `drmAddMap`, `drmAgpAcquire`, `drmAgpAlloc`, `drmAgpBase`, `drmAgpBind`, `drmAgpDeviceId`, `drmAgpEnable`, `drmAgpFree`, `drmAgpGetMode`, `drmAgpMemoryAvail`, `drmAgpMemoryUsed`, `drmAgpRelease`, `drmAgpSize`, `drmAgpUnbind`, `drmAgpVendorId`, `drmAgpVersionMajor`, `drmAgpVersionMinor`, `drmAuthMagic`, `drmAvailable`, `drmClose`, `drmCloseBufferHandle`, `drmCloseOnce`, `drmCommandNone`, `drmCommandRead`, `drmCommandWrite`, `drmCommandWriteRead`, `drmCreateContext`, `drmCreateDrawable`, `drmCrtcGetSequence`, `drmCrtcQueueSequence`, `drmCtlInstHandler`, `drmCtlUninstHandler`, `drmDMA`, `drmDelContextTag`, `drmDestroyContext`, `drmDestroyDrawable`, `drmDevicesEqual`, `drmDropMaster`, `drmError`, `drmFinish`, `drmFree`, `drmFreeBufs`, `drmFreeBusid`, `drmFreeDevice`, `drmFreeDevices`, `drmFreeReservedContextList`, `drmFreeVersion`, `drmGetBufInfo`, `drmGetBusid`, `drmGetCap`, `drmGetClient`, `drmGetContextFlags`, `drmGetContextPrivateMapping`, `drmGetContextTag`, `drmGetDevice`, `drmGetDevice2`, `drmGetDeviceFromDevId`, `drmGetDeviceNameFromFd`, `drmGetDeviceNameFromFd2`, `drmGetDevices`, `drmGetDevices2`, `drmGetEntry`, `drmGetFormatModifierName`, `drmGetFormatModifierVendor`, `drmGetFormatName`, `drmGetHashTable`, `drmGetInterruptFromBusID`, `drmGetLibVersion`, `drmGetLock`, `drmGetMagic`, `drmGetMap`, `drmGetNodeTypeFromDevId`, `drmGetNodeTypeFromFd`, `drmGetPrimaryDeviceNameFromFd`, `drmGetRenderDeviceNameFromFd`, `drmGetReservedContextList`, `drmGetStats`, `drmGetVersion`, `drmHandleEvent`, `drmHashCreate`, `drmHashDelete`, `drmHashDestroy`, `drmHashFirst`, `drmHashInsert`, `drmHashLookup`, `drmHashNext`, `drmIoctl`, `drmIsMaster`, `drmMalloc`, `drmMap`, `drmMapBufs`, `drmMarkBufs`, `drmMsg`, `drmOpen`, `drmOpenControl`, `drmOpenOnce`, `drmOpenOnceWithType`, `drmOpenRender`, `drmOpenWithType`, `drmPrimeFDToHandle`, `drmPrimeHandleToFD`, `drmRandom`, `drmRandomCreate`, `drmRandomDestroy`, `drmRandomDouble`, `drmRmMap`, `drmSLCreate`, `drmSLDelete`, `drmSLDestroy`, `drmSLDump`, `drmSLFirst`, `drmSLInsert`, `drmSLLookup`, `drmSLLookupNeighbors`, `drmSLNext`, `drmScatterGatherAlloc`, `drmScatterGatherFree`, `drmSetBusid`, `drmSetClientCap`, `drmSetContextFlags`, `drmSetInterfaceVersion`, `drmSetMaster`, `drmSwitchToContext`, `drmSyncobjCreate`, `drmSyncobjDestroy`, `drmSyncobjEventfd`, `drmSyncobjExportSyncFile`, `drmSyncobjFDToHandle`, `drmSyncobjHandleToFD`, `drmSyncobjImportSyncFile`, `drmSyncobjQuery`, `drmSyncobjQuery2`, `drmSyncobjReset`, `drmSyncobjSignal`, `drmSyncobjTimelineSignal`, `drmSyncobjTimelineWait`, `drmSyncobjTransfer`, `drmSyncobjWait`, `drmUnlock`, `drmUnmap`, `drmUnmapBufs`, `drmUpdateDrawableInfo`, `drmWaitVBlank`
- xf86drmMode.h: `drmCheckModesettingSupported`, `drmIsKMS`, `drmModeAddFB`, `drmModeAddFB2`, `drmModeAddFB2WithModifiers`, `drmModeAtomicAddProperty`, `drmModeAtomicAlloc`, `drmModeAtomicCommit`, `drmModeAtomicDuplicate`, `drmModeAtomicFree`, `drmModeAtomicGetCursor`, `drmModeAtomicMerge`, `drmModeAtomicSetCursor`, `drmModeAttachMode`, `drmModeCloseFB`, `drmModeConnectorGetPossibleCrtcs`, `drmModeConnectorSetProperty`, `drmModeCreateDumbBuffer`, `drmModeCreateLease`, `drmModeCreatePropertyBlob`, `drmModeCrtcGetGamma`, `drmModeCrtcSetGamma`, `drmModeDestroyDumbBuffer`, `drmModeDestroyPropertyBlob`, `drmModeDetachMode`, `drmModeDirtyFB`, `drmModeFormatModifierBlobIterNext`, `drmModeFreeConnector`, `drmModeFreeCrtc`, `drmModeFreeEncoder`, `drmModeFreeFB`, `drmModeFreeFB2`, `drmModeFreeModeInfo`, `drmModeFreeObjectProperties`, `drmModeFreePlane`, `drmModeFreePlaneResources`, `drmModeFreeProperty`, `drmModeFreePropertyBlob`, `drmModeFreeResources`, `drmModeGetConnector`, `drmModeGetConnectorCurrent`, `drmModeGetConnectorTypeName`, `drmModeGetCrtc`, `drmModeGetEncoder`, `drmModeGetFB`, `drmModeGetFB2`, `drmModeGetLease`, `drmModeGetPlane`, `drmModeGetPlaneResources`, `drmModeGetProperty`, `drmModeGetPropertyBlob`, `drmModeGetResources`, `drmModeListLessees`, `drmModeMapDumbBuffer`, `drmModeMoveCursor`, `drmModeObjectGetProperties`, `drmModeObjectSetProperty`, `drmModePageFlip`, `drmModePageFlipTarget`, `drmModeRevokeLease`, `drmModeRmFB`, `drmModeSetCrtc`, `drmModeSetCursor`, `drmModeSetCursor2`, `drmModeSetPlane`


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
