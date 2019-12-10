#!/bin/bash
set -e
sln=../src/Resume.sln
proj=../src/Resume/Resume.csproj
config=Release
runtime=win-x64
dist=../dist/Resume
exe=../src/Resume/

# Clean
echo Cleaning...
rm -f -r $dist
dotnet clean $sln -c $config -r $runtime

# Build
echo Building solution...
dotnet build  $sln -c $config -r $runtime --no-restore

# Run unit tests
echo Running tests...
dotnet test  $sln -c $config -r $runtime

# Create distribution
echo Creating distribution...
mkdir -p $dist
dotnet publish $proj -c $config -r $runtime //p:PublishTrimmed=true //p:PublishReadyToRun=true //p:PublishSingleFile=true -o $dist
cp ../LICENSE.txt $dist
rm -f ../dist/Resume/*.pdb

# Build succeeded
echo Build successful
exit 0
