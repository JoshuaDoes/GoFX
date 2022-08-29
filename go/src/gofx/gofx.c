#include <windows.h>
#include <stdbool.h>
#include <stdio.h>
#include "gofx.h"

// https://forum.pellesc.de/index.php?topic=4725.0
#ifdef __GNUC__
//HANDLE kGetResourceName __attribute__((section(".shared"), shared)) = NULL;
#endif
#ifdef _MSC_VER
#pragma data_seg(".shared")
//HANDLE kGetResourceName = NULL;
#pragma data_seg()
#pragma comment(linker, "/section:.shared,RWS")
#endif

typedef char* (*goGetResourceName)();
typedef void (*goOnResourceStart)(GoString);
typedef void (*goOnResourceStarting)(GoString);
typedef void (*goOnResourceStop)(GoString);

HMODULE hGoFX = NULL;

bool __declspec(dllexport) InitAssembly(const char* assemblyPath, const char* assemblyName) {
    AddDllDirectory(assemblyPath);
    hGoFX = LoadLibrary(assemblyName);
    if (hGoFX == NULL) {
        return FALSE;
    }
    return TRUE;
}

bool __declspec(dllexport) Hello() {
    return TRUE;
}

bool __declspec(dllexport) GoOnResourceStart(GoString resourceName) {
    goOnResourceStart callOnResourceStart = (goOnResourceStart)GetProcAddress(hGoFX, "OnResourceStart");
    callOnResourceStart(resourceName);
}

BOOL APIENTRY DllMain(HMODULE hModule,
                      DWORD ul_reason_for_call,
                      LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    {
        /*kHello = Hello;
        kSetAssembly = SetAssembly;
        kInitAssembly = InitAssembly;*/

        // do not execute the function here!
        break;
    }

    case DLL_PROCESS_DETACH:
    {
        if (hGoFX)
            FreeLibrary(hGoFX);
        break;
    }
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    default:
        break;
    }
    return TRUE;
}
