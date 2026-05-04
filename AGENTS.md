# Orc.SystemInfo

Orc.SystemInfo is a library used to retrieve system information details from a computer. It provides services for querying CPU, memory, operating system, .NET framework versions, WMI data, and more.

---

## Critical Rules (Read First)

These rules are **non-negotiable**. Violating them causes broken builds, crashes, or downstream breakage.

### 1. ABI / API Stability

This project maintains stable ABI / API. Breaking changes break downstream apps.

| Allowed | Never |
|---------|-------|
| Add new overloads | Modify existing signatures |
| Add new methods | Remove public APIs |
| Add new classes | Change return types |

### 2. Tests Are Mandatory

**Building alone is NOT sufficient.** Run tests before claiming completion (see [Commands](#commands)).

### 3. Branch Protection (COMPLIANCE REQUIRED)

**Direct commits to protected branches are a policy violation.**

| Repository | Protected Branches |
|------------|-------------------|
| Orc.SystemInfo | `master` |
| Orc.SystemInfo | `develop` |

**Required workflow:**

1. **Create a feature branch FIRST** — Use naming convention: `feature/issue-NNNN-description`
2. **Make all commits on the feature branch** — Never commit directly to protected branches
3. **Submit a Pull Request** — Changes must be reviewed by a human before merging

```bash
# CORRECT — Always create a feature branch first
git checkout -b feature/issue-1234-fix-description

# NEVER DO THIS — Policy violation
git checkout develop && git commit  # FORBIDDEN

# NEVER DO THIS — Policy violation
git checkout master && git commit  # FORBIDDEN
```

---

## Commands

Single source of truth for all commands:

| Task | Command |
|------|---------|
| **Build** | `dotnet cake --target=build` |
| **Test** | `dotnet cake --target=test` |
| **Build and test** | `dotnet cake --target=buildandtest` |

---

## Architecture & Directories

### Solution Overview

```
Orc.SystemInfo          => Core library — retrieves system information
Orc.SystemInfo.Example  => Example application
Orc.SystemInfo.Tests    => Unit and integration tests
WbemShim                => Native WMI shim (x86/x64 DLLs embedded as resources)
```

### Directory Guide

| Directory / File | Editable? | Notes |
|-----------------|-----------|-------|
| `src/Orc.SystemInfo/Services/` | Yes | Core service implementations |
| `src/Orc.SystemInfo/Services/Interfaces/` | Yes | Public service interfaces |
| `src/Orc.SystemInfo/Services/Providers/` | Yes | System info data providers |
| `src/Orc.SystemInfo/Models/` | Yes | Data model classes |
| `src/Orc.SystemInfo/Win32/` | Yes | Win32 interop helpers |
| `src/Orc.SystemInfo/Wmi/` | Yes | WMI-related wrappers |
| `src/Orc.SystemInfo.Tests/` | Yes | Test project |
| `deployment/` | No | Deployment / build scripts |
| `src/WbemShim/` | No | Native WMI shim — do not modify |
| `src/Orc.SystemInfo/costura-win-x64/` | No | Embedded native DLLs |
| `src/Orc.SystemInfo/costura-win-x86/` | No | Embedded native DLLs |

### Target Frameworks

The library targets `net8.0`, `net9.0`, and `net10.0`.

---

## Writing Code

### Anti-Patterns (Never Do This)

| Anti-Pattern | Why |
|-------------|-----|
| Modifying method signatures | ABI breaking |
| Using default parameters in public APIs | ABI breaking |
| **Skipping failing tests** | **Unacceptable — tests must pass** |

---

## Testing & Debugging

### Running Tests

```bash
dotnet cake --target=test
```

### Tests MUST Pass

> **NON-NEGOTIABLE:** Tests must PASS before claiming completion.
>
> - Do NOT skip failing tests
> - Do NOT claim completion if tests fail
> - Do NOT use `SkipException` to work around failures

### Writing Tests

1. Use NUnit to write tests
2. Create a Facts class for a feature
3. Combine Pascal / Snake case for test methods (e.g. `Feature_Does_Work`)

```csharp
[Test]
public void Feature_Does_Work()
{
    var result = 47 - 5;

    Assert.That(result, Is.EqualTo(42));
}
```

**Philosophy:** Tests FAIL when wrong, never skip (except missing hardware).

### Debugging Methodology

1. **Establish baseline** — What's the known-good state?
2. **One change at a time** — Verify each change before proceeding
3. **Track changes in a table** — Log what you changed and the result
4. **Platform differences are signals** — If X works and Y fails, the difference IS the answer
5. **Revert if worse** — Don't pile fixes on top of failures

---

## Further Reading

| Topic | Document |
|-------|----------|
| Contributing guidelines | [CONTRIBUTING.md](CONTRIBUTING.md) |
| Documentation portal | http://opensource.wildgums.com |
