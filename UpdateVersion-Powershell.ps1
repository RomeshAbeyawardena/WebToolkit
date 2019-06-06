Param(
    [string]$fileName,
    [string]$version
)

[XML]$val = Get-Content $fileName
$propertyGroupNode = $val | Select-Xml -XPath "//Project//PropertyGroup" 
$properties = $propertyGroupNode.Node
Write-Output "Updating $fileName to $version..."
$properties.Version = $version
$properties.AssemblyVersion = $version
$properties.FileVersion = $version
$val.Save("Directory.Build.Props")
Write-Output "Updating $fileName to $version successful!"
