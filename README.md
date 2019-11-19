# BottomSheet for Xamarin Forms

 Is a cross platform control for [Xamarin.Forms](https://www.xamarin.com/forms) which help to show Xamarin.Forms view as a popup that can be shared across all platforms.
It's based on the PopupDialog idea of [Javier Suárez](https://javiersuarezruiz.wordpress.com/2019/07/17/xamarin-ui-challenge-art-plant-mall/)
 ===================


BottomSheet for Xamarin.Forms. Work with .net Standard and use Xamarin.Forms dependecy.

Install on .Net Standard Library.

## NuGet
* Available on NuGet: [BottomSheetXF](https://www.nuget.org/packages/BottomSheetXF/)


## DEMO
![](https://media.giphy.com/media/co0eVhB8LxRV9n0GX4/giphy.gif)

## DEMO - PLAY WIHT POSITONS
![play with movements and content positions](https://media.giphy.com/media/gk3JjswtSfCHQdDJzB/giphy.gif)
----------


Properties
-------------


> - IsOpen : you can open or close the popup with this property.
> - ParentHeight : you need set the parent height to this parameter in the code behind.
> - FadeBackgroundEnabled : hide or show a fadeBackground. DEFAULT:true
> - FadeColor : Color of Fade. DEFAULT: #AA000000
> - Movement : ENUM with the movement of the popup. BottomUp,TopBottom,LeftRight,RightLeft. DEFAULT: BottomUp
> - ContentPosition : ENUM with the position of the content in the popup. Bottom,Top, Left,Right. DEFAULT: Bottom
> - Duration: Show the Snackbar for a DURATION milliseconds. (ONLY SnackBarBottomSheet)


How use it
-------------

##xaml ##

CUSTOM BOTTOMSHEET
-------------

> Create you popup as ContentView.
> Import the Core.
> Import : `xmlns:bottomSheet="clr-namespace:BottomSheet.Core;assembly=BottomSheetXF""`

> Replace: replace you base ContentView to  : 

	<bottomSheet:BottomSheetDialog

> Replace the content and put something like this:

```xaml
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
```

----------


SIMPLE BOTTOMSHEET
-------------

> - Import the Implementations: `xmlns:bottomSheet="clr-namespace:BottomSheet.Implementations;assembly=BottomSheetXF"`
> - Use:
```xaml
        <bottomSheet:SimpleBottomSheet 
            x:Name="SimpleBottomSheet" 
            Title="What is Lorem Ipsum?" 
            Body="Lorem Ipsum is simply dummy text of the printing and typesetting 
                industry. Lorem Ipsum has been the industry's standard dummy text ever 
                since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." 
            MainPadding="20"
            IsOpen="{Binding IsOpenSimple}"
            MainColor="LightGreen"/>
```

----------


SNACKBAR BOTTOMSHEET
-------------

> - Import the Implementations: `xmlns:bottomSheet="clr-namespace:BottomSheet.Implementations;assembly=BottomSheetXF"`
> - Use:
```xaml
        <bottomSheet:SnackBarBottomSheet 
            x:Name="SnackBarBottomSheet"
            Title="Hii, i am a SnackBar with a duration"
            MainColor="LightSkyBlue"
            Duration="3000"
            MainPadding="20"
            IsOpen="{Binding IsOpenSimpleSnackBar}"/>
```

## NOTE
> - On the page where you go to use it, you must send the height of the page.

```csharp
protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (!SimpleBottomSheet.IsInitiated)
            {
                SimpleBottomSheet.Init(height, width);
            }
        }
```