param(
    [string]$target
    [string]$out = "output"
)

    Invoke-WebRequest -Uri $target -Outfile $out
