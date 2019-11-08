# BottomSheet for Xamarin Forms
 Is a cross platform plugin for [Xamarin.Forms](https://www.xamarin.com/forms) which help to show Xamarin.Forms view as a popup that can be shared across all platforms.
 ===================


BottomSheet for Xamarin.Forms. Work with .net Standard and use Xamarin.Forms dependecy.

Install on .Net Standard Library.

## NuGet
* Available on NuGet: [BottomSheetXF](https://www.nuget.org/packages/BottomSheetXF/)


----------


Properties
-------------


> - IsOpen : you can open or close the popup with this property.
> - ParentHeight : you need set the parent height to this parameter in the code behind.

How use it
-------------

##xaml ##

> Create you popup as ContentView.
> Import : `xmlns:bottomSheet="clr-namespace:BottomSheet;assembly=BottomSheetXF""`

> Replace: replace you base ContentView to  : 

	<bottomSheet:BottomSheetDialog

> Replace the content and put something like this:


	 	<bottomSheet:BottomSheetDialog.View>
        	<ContentView>
            	<StackLayout
                	BackgroundColor="LightBlue"
                	Padding="50">
                	<Label Text="HELLO, i  am a title" TextColor="Black" FontSize="Title"/>
                	<Label Text="I am a body" 
                       TextColor="Black"
                       FontSize="Body"/>
            	</StackLayout>
        	</ContentView>
    	</bottomSheet:BottomSheetDialog.View>