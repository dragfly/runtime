# Updating JitInterface

JitInterface is the binary interface that is used to communicate with the JIT. The bulk of the interface consists of the ICorStaticInfo and ICorDynamicInfo interfaces and enums/structs used by those interfaces.

Following header files define parts of the JIT interface: cordebuginfo.h, corinfo.h, corjit.h, corjitflags.h, corjithost.h.

The JitInterface serves two purposes:
* Standardizes the interface between the runtime and the JIT (potentially allowing mixing and matching JITs and runtimes)
* Allows the JIT to be used elsewhere (outside of the runtime)

There are several components that consume the JIT outside of the runtime. Since those components don't consume the JIT using the header, changes to the JIT have to be ported manually.

The JitInterface is versioned by a GUID. Any change to JitInterface is required to update the JitInterface GUID located in corinfo.h (look for `JITEEVersionIdentifier`). Not doing so has consequences that are sometimes hard to debug.

## Porting JitInterface changes to crossgen2

Crossgen2 is the AOT compiler for CoreCLR. It generates native code for .NET apps ahead of time and uses the JIT to do that. Since crossgen2 is written in managed code, it doesn't consume the C++ headers and maintains a managed copy of them. Changes to JitInterface need to be ported managed code.

1. If an enum/struct was modified or added, port the change to CorInfoTypes.cs.
2. If a method was added or modified in ICorStaticInfo or ICorDynamicInfo, port the change to ThunkInput.txt.
   Run gen.bat or gen.sh to regenerate CorInfoBase.cs and jitinterface.h from ThunkInput.txt. Provide a managed implementation of the method in CorInfoImpl.cs. If the managed implementation is specific to CoreCLR ReadyToRun (and doesn't apply to full AOT compilation), provide the implementation in CorInfoImpl.ReadyToRun.cs instead.
3. Update JitInterface GUID in jitwrapper.cpp.

