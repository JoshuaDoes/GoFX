package main

/*
#include <windows.h>
#include <stdlib.h>
*/
import "C"
//import "unsafe"

import (
	"encoding/json"
	"fmt"
	"time"
)

type Event struct {
	Name string `json:"event"`
	Params []json.RawMessage `json:"params"`
}
func (e *Event) ToJSON() []byte {
	bytes, err := json.Marshal(e)
	if err == nil {
		return bytes
	}
	return make([]byte, 0)
}

var eventQueue []*Event
var eventRecv []*Event

//export GetNextEvent
func GetNextEvent() []byte {
	if len(eventQueue) == 0 {
		return nil
	}
	//TODO: Remove index 0 from queue
	return eventQueue[0].ToJSON()
}

//export WriteNextEvent
func WriteNextEvent(event []byte) bool {
	e := &Event{}
	err := json.Unmarshal(event, e)
	if err == nil {
		eventRecv = append(eventRecv, e)
		return true
	}
	return false
}

func CStr(str string) *C.char {
	msg := []byte(str)
	msg = append(msg, '\x00') //Null-terminate the strings!
	cMsg := C.CBytes(msg)
	cChar := (*C.char)(cMsg)
	return cChar
}

//export GetResourceName
func GetResourceName() *C.char {
	return CStr("GoFX test resource")
}

//export OnResourceStart
func OnResourceStart(resourceName string) {
	fmt.Printf("Successfully started %s at %v\n", resourceName, time.Now().Unix())
}

//export OnResourceStarting
func OnResourceStarting(resourceName string) {
	fmt.Printf("Successfully starting %s at %v\n", resourceName, time.Now().Unix())
}

//export OnResourceStop
func OnResourceStop(resourceName string) {
	fmt.Printf("Successfully stopped %s at %v\n", resourceName, time.Now().Unix())
}

func main() {}