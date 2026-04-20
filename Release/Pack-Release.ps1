#$VerbosePreference = "Continue";
$7Zip = "C:\Program Files\7-Zip\7z.exe";
$7ZipParams = "-mx=0"; # Compression level is NONE/COPY
$ExcludeFiles = "*.ps1", "*.zip";

$FilesToPack = Get-Childitem -Exclude $ExcludeFiles;

$Version = Read-Host -Prompt "Version (#.#)";
$ArchiveName = "Excel Add-In $Version.zip";

foreach($FileObject in $FilesToPack)
{
	& $7Zip a $ArchiveName "$($FileObject.FullName)" $7ZipParams | Out-Null;
	Write-Verbose -Message "Packing $FileObject into $ArchiveName";
}

$RegexPattern = "Everything is Ok";
[bool]$TestResult = & $7Zip t "$ArchiveName" | Out-String | Select-String -Pattern $RegexPattern -Quiet;
Write-Verbose -Message "Archive Test of $ArchiveName evaluates to boolean $TestResult";

if(($null -eq $TestResult) -or ($false -eq $TestResult))
{
	Write-Error -Message "Archive Test Failed!" -RecommendedAction "Check files and attempt to pack again" -Category InvalidResult -CategoryActivity "Result";
	Read-Host -Prompt "Press ENTER to continue";
}
else
{
	$ShouldRemove = Read-Host -Prompt "Remove all files from Release directory?[y]";
	if([string]::IsNullOrEmpty($ShouldRemove) -or ($ShouldRemove -match "y"))
	{
		foreach($FileObject in $FilesToPack)
		{
			Write-Verbose -Message "Removing $FileObject";
			Remove-Item $FileObject -Recurse;
		}
	}
}
