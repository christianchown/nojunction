# nojunction

Scans a Windows directory and replaces < JUNCTION > symlinks with real files.

Needed because RN0.57 on my Windows setup has the error

> "Failed to capture snapshot of input files for task ':app:bundleReleaseJsAndAsset s' property '$1' during up-to-date check."

See:

https://stackoverflow.com/questions/53104843/react-native-could-not-read-path-babel-core-node-modules-json5-bin-json5