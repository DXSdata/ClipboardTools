# Clipboard Tools for .NET Framework
Collection of tools for advanced handling of Clipboard data (virtual files), change notifications etc.


Look for the included Sample project to get a fully working demo.

# Sample


```CSharp
	using ClipboardTools;
	
	//...
	
	Notification.ClipboardUpdate += Notification_ClipboardUpdate;
	
	//...

        if (!VirtualFiles.ContainsVirtualFiles)
           Log("Clipboard does not contain virtual files.");
           
        //...
        
        var streams = VirtualFiles.GetVirtualFilesAsStreams();
        
        var files = VirtualFiles.GetVirtualFilesAsFiles();
        
        //...
           
        var ostreams = VirtualFiles.GetOutlookVirtualFilesAsStreams();
        
        var ofiles = VirtualFiles.GetOutlookVirtualFilesAsFiles();
        

```

# Links
- Website http://www.dxsdata.com/2018/02/net-clipboard-tools/