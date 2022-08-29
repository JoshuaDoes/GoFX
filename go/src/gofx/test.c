#include <winsock2.h>
#include <windows.h>
#include <stdio.h>
#include <memory.h>

// define types needed
typedef long long go_int;
typedef double go_float64;
typedef struct{void *arr; go_int len; go_int cap;} go_slice;
typedef struct{const char *p; go_int len;} go_str;

typedef HMODULE (*GetStarterGetResourceName)();
typedef HMODULE (*GetStarterOnResourceStart)();
typedef HMODULE (*GetStarterOnResourceStarting)();
typedef HMODULE (*GetStarterOnResourceStop)();

typedef char* (*GoGetResourceName)();
typedef void (*GoOnResourceStart)(go_str);
typedef void (*GoOnResourceStarting)(go_str);
typedef void (*GoOnResourceStop)(go_str);

int main()
{
	HMODULE hGoFX = NULL;

	printf("Loading GoFX...\n");
	hGoFX = LoadLibrary("GoFX.dll");
	GetStarterGetResourceName getStarterGetResourceName = (GetStarterGetResourceName)GetProcAddress(hGoFX, "GetStarterGetResourceName");
	GoGetResourceName goGetResourceName = (GoGetResourceName)getStarterGetResourceName();
	GetStarterOnResourceStart getStarterOnResourceStart = (GetStarterOnResourceStart)GetProcAddress(hGoFX, "GetStarterOnResourceStart");
	GoOnResourceStart goOnResourceStart = (GoOnResourceStart)getStarterOnResourceStart();
	GetStarterOnResourceStarting getStarterOnResourceStarting = (GetStarterOnResourceStarting)GetProcAddress(hGoFX, "GetStarterOnResourceStarting");
	GoOnResourceStarting goOnResourceStarting = (GoOnResourceStarting)getStarterOnResourceStarting();
	GetStarterOnResourceStop getStarterOnResourceStop = (GetStarterOnResourceStop)GetProcAddress(hGoFX, "GetStarterOnResourceStop");
	GoOnResourceStop goOnResourceStop = (GoOnResourceStop)getStarterOnResourceStop();

	char* resourceName = goGetResourceName();
	go_str goResourceName = {resourceName, strlen(resourceName)};

	goOnResourceStart(goResourceName);
	goOnResourceStarting(goResourceName);
	goOnResourceStop(goResourceName);

	free(resourceName);

	FreeLibrary(hGoFX);
	return 0;
}