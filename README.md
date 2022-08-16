# Block-rename
A rough-and-ready file renaming utility that gives all files in the same folder sequential names.

## Usage

```
BlockRename.exe -f folder -b base
```

takes all the files in `folder`, sorts them alphabetically, and renames them `base_0000.ext`, `base_0001.ext` and so on (wtih `.ext` being the original extension).  The program will print a list of each rename it does.

If the renaming would cause data loss, for example if one or more of the target names already exists in the folder and would be overwritten by the renaming operation, the program will print an error message and exit.
If you get this error message, try renaming all the files with a different base name first, then running it again with your intended base name.

## What's this for?

I've been using the video editing and post-production suite [DaVinci Resolve](https://www.blackmagicdesign.com/products/davinciresolve) for producing little videos including snippets of stop-motion animation.  
One handy feature of Resolve is that it will automatically compile sequences of still images into film clips *as long as* the still files are all named with the right convention: they must all start with the
same prefix, and end with a sequential number without any gaps in the sequence.

If you're using a digital camera that follows the DCF standard this is mostly fine, because it matches the file-naming part of the DCF standard; if you've taken any files out of the sequence it splits your film
into two clips, but that's liveable.  However if you're using something that doesn't follow the DCF standard&mdash;like my phone, which gives each image file a name derived from its timestamp such as 
`20220816_070112.jpg`&mdash;then it sees them all as completely separate images.

Because renaming a whole bunch of image files is a pain when you have 24 images per second of film, I wrote this tool.  Put all the files that are to make up one film clip into their own folder.  Do any tweaks to the
file naming needed to put them into the right order, if you have to.  Run `BlockRename.exe` and the files all have names which will make sure Resolve, importing in Sequence mode, sees them all as a single clip.

## Requirements

This tool is written in C# for .NET 6, and although it's only been tested on Windows 11, should be fully cross-platform.

## Issues and things to watch out for

I haven't found any issues as *yet*, but I haven't tested the program very thoroughly.  Make a backup copy of your files before running it.

Any errors caused by IO or file permissions issues will generally cause the program to exit mid-progress, with some files already renamed.

The program sorts existing filenames according to the .NET "invariant culture", so bear this in mind if your source files have a variety of names.

## Potential enhancements

- Starting the numbering sequence at numbers other than zero ([issue #1](https://github.com/caitlinsalt/block-rename/issues/1)).
- Optionally copying instead of renaming ([issue #2](https://github.com/caitlinsalt/block-rename/issues/2)).
- Silent option ([issue #3](https://github.com/caitlinsalt/block-rename/issues/3)).
- Unit tests ([issue #4](https://github.com/caitlinsalt/block-rename/issues/4)).
- Integration tests ([issue #5](https://github.com/caitlinsalt/block-rename/issues/5)).
- Publish a compiled package ([issue #6](https://github.com/caitlinsalt/block-rename/issues/6)).
