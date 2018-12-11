& .\JDI.Light\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:"nunit3-console.exe" "-targetargs:""JDI.Light\JDI.Light.Tests\bin\Release\JDI.Light.Tests.dll""" -filter:"+[JDI*]*" -output:opencoverCoverage.xml

$coveralls = (Resolve-Path "JDI.Light/packages/coveralls.net.*/tools/csmacnz.coveralls.exe").ToString()

write-output $coveralls
write-output $env:APPVEYOR_PULL_REQUEST_NUMBER

if ($env:APPVEYOR_PULL_REQUEST_NUMBER -eq "") {
	& $coveralls --opencover -i opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_JOB_ID
}
else {
	& $coveralls --opencover -i opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_JOB_ID --pullRequest $env:APPVEYOR_PULL_REQUEST_NUMBER
}