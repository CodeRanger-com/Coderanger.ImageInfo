# Contributions

Contributions are welcome; even if it is just additional test images for subtypes of an image format so that the test suite can be increased.

## Commit Messages

This project employs the semantic release semantics for commit messages, it is Angular-esque in that there is a type, section and message to each commit message summary in the format `type(Section): Message Summary`

Use the second line of the commit message to describe in further detail the changes which have been made and why.

The release workflow will look at all the commit messages since the last tagged release and will generate a new SemVer release version number based on the highest `type` discovered.

The different types available will reflect the part of the SemVer release number generated: `<major>.<minor>.<patch>` but also how the `CHANGELOG.md` file is automatically created.

### Types Supported

**Major:**
* breaking
* backwards-incompatible
* major

**Minor:**
* feat
* feature
* minor
* perf

**Patch:**
* fix
* patch
* refactor
* revert
* style
* build
* docs
* test
* other

**No version bump:**
* chore
* skip ci

You can also add `skip ci` anywhere in the message for it to be ignored as a semantic release commit.

### Some Examples

```
feature(Memory): Converted to use Span<T>
To avoid unnecessary allocations and thus garbage collection pressure
```

```
patch(WebP Fix): Handling of VP8X image resolution
Previously, was retrieving the image resolution incorrectly
```

```
breaking(Public API): Method name changed
To avoid confusion, the public api to retrieve image info has
been modified to better reflect its usage.
```

```
chore(Workflows): Modified pull request workflow
Added checking of commit message semantics
```
