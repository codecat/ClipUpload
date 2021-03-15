ClipUpload 3
============

No longer maintained.

General Info
------------
The Visual Studio 2010 solution for ClipUpload 3 has 11 projects associated to it:
 - ClipUpload3: The program itself
 - AddonHelper: This contains a class that is to be inherited by addons for extra helper methods.
 - GlobalHook: Library that handles global Windows hooks.
 - Dropbox: The Dropbox addon
 - FTP: The FTP addon
 - Imgur: The Imgur addon
 - Pastebin: The Pastebin addon
 - PostHttp: The http POST method addon
 - Snaggy: The optional Snag.gy addon
 - TestAddon: Addon for testing. Merely used for testing new methods in AddonHelper.

Build Info
----------
Each addon project has been set up to build to the ClipUpload3\bin\Debug\Addons folder. You will probably need to set up every config file yourself in the bin\Debug folder. Get them from your installation, as it's nearly impossible for me to include clean files in this repository :)
