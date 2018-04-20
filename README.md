[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.Drawing/master/Shared/NuGet/Icon.png "Zebble.Drawing"


## Zebble.Drawing

![logo]

A Zebble plugin that allow you to draw shapes in Zebble applications.


[![NuGet](https://img.shields.io/nuget/v/Zebble.Drawing.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.Drawing/)

> The Drawing Plugin is a canvas that allows you to create drawings containing lines or polygon shapes.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.Drawing/](https://www.nuget.org/packages/Zebble.Drawing/)
* Install in your platform client projects.
* Available for iOS, Android and UWP.
<br>


### Api Usage

To draw shapes you can use this plugin like code below:
```xml
<Drawing Id="MyDrawing">
     <Drawing.Line Start="100, 100" End="200, 200" Color="#000000" />
     <Drawing.Polygon Id="MySquare" LineColor="#ff0000" LineThickness="3" FillColor="#ffffff">
            <Point X="0" Y="0" />
            <Point X="100" Y="0" />
            <Point X="100" Y="100" />
            <Point X="0" Y="100" />
     </Drawing.Polygon>
</Drawing>
```
##### Default value of some properties:

`FillColor` will be `transparent` <br>
`LineColor` will be `black` <br>
`LineThickness` will be `1` <br>
#### Creating by code:
Of course you can create a drawing with code, like any other Zebble object. For example:
```csharp
var myDrawing = new Drawing();
myDrawing.Add(new Line(new Point(0, 0), new Point(100, 0)));
```
### Properties
| Property     | Type         | Android | iOS | Windows |
| :----------- | :----------- | :------ | :-- | :------ |
| FillColor            | Color           | x       | x   | x       |
| LineColor            | Color           | x       | x   | x       |
| LineThickness            | int           | x       | x   | x       |


### Events
| Event             | Type                                          | Android | iOS | Windows |
| :-----------      | :-----------                                  | :------ | :-- | :------ |
| LineAdded               | AsyncEvent<Line&gt;    | x       | x   | x       |
| PolygonAdded              | AsyncEvent<Polygon&gt;    | x       | x   | x       |
| Cleared              | AsyncEvent    | x       | x   | x       |

### Methods
| Method       | Return Type  | Parameters                          | Android | iOS | Windows |
| :----------- | :----------- | :-----------                        | :------ | :-- | :------ |
| Add         | Task| line -> Line OR polygon -> Polygon | x       | x   | x       |
| Clear         | Task| -| x       | x   | x       |
