#include <comdef.h>
#include <combaseapi.h>
#include "WbemShim.h"
#include <objidl.h>

extern "C" __declspec(dllexport) HRESULT BlessIWbemServices(IUnknown * pInterface,
    DWORD dwImpLevel,
    DWORD dwAuthnLevel);

HRESULT BlessIWbemServices(
    IUnknown *pInterface,
    DWORD dwImpLevel,
    DWORD dwAuthnLevel)
{
    DWORD dwAuthnSvc = RPC_C_AUTHN_DEFAULT;
    DWORD dwAuthzSvc = RPC_C_AUTHZ_DEFAULT;
    return CoSetProxyBlanket(pInterface, dwAuthnSvc, dwAuthzSvc, NULL, dwAuthnLevel, dwImpLevel, NULL, EOLE_AUTHENTICATION_CAPABILITIES::EOAC_DEFAULT);
}
