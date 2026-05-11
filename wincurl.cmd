powershell -Command "& {param([string]$target, [string]$out='output') Invoke-WebRequest -Uri $target -OutFile $out}" -target "https://example.com" -out "myfile.zip"
