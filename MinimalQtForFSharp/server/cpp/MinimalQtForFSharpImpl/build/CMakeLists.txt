cmake_minimum_required(VERSION 3.14)

project(MinimalQtForFSharpImpl LANGUAGES CXX)

set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

set(CMAKE_POSITION_INDEPENDENT_CODE ON)

find_package(QT NAMES Qt6 Qt5 REQUIRED COMPONENTS Widgets)
find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS Widgets)

add_library(MinimalQtForFSharpImpl STATIC
    ../support/NativeImplServer.cpp
    ../generated/Library.cpp

    ../generated/AbstractButton.h
    ../generated/AbstractButton_wrappers.cpp
    ../AbstractButton.cpp

    ../generated/AbstractItemDelegate.h
    ../generated/AbstractItemDelegate_wrappers.cpp
    ../AbstractItemDelegate.cpp

    ../generated/AbstractItemModel.h
    ../generated/AbstractItemModel_wrappers.cpp
    ../AbstractItemModel.cpp

    ../generated/AbstractItemView.h
    ../generated/AbstractItemView_wrappers.cpp
    ../AbstractItemView.cpp

    ../generated/AbstractListModel.h
    ../generated/AbstractListModel_wrappers.cpp
    ../AbstractListModel.cpp

    ../generated/AbstractProxyModel.h
    ../generated/AbstractProxyModel_wrappers.cpp
    ../AbstractProxyModel.cpp

    ../generated/AbstractScrollArea.h
    ../generated/AbstractScrollArea_wrappers.cpp
    ../AbstractScrollArea.cpp

    ../generated/AbstractSlider.h
    ../generated/AbstractSlider_wrappers.cpp
    ../AbstractSlider.cpp

    ../generated/Action.h
    ../generated/Action_wrappers.cpp
    ../Action.cpp

    ../generated/Application.h
    ../generated/Application_wrappers.cpp
    ../Application.cpp

    ../generated/BoxLayout.h
    ../generated/BoxLayout_wrappers.cpp
    ../BoxLayout.cpp

    ../generated/CalendarWidget.h
    ../generated/CalendarWidget_wrappers.cpp
    ../CalendarWidget.cpp

    ../generated/Color.h
    ../generated/Color_wrappers.cpp
    ../Color.cpp
    ../ColorInternal.h

    ../generated/ComboBox.h
    ../generated/ComboBox_wrappers.cpp
    ../ComboBox.cpp

    ../generated/Common.h
    ../generated/Common_wrappers.cpp
    ../Common.cpp
    ../CommonInternal.h

    ../generated/Cursor.h
    ../generated/Cursor_wrappers.cpp
    ../CursorInternal.h
    ../Cursor.cpp

    ../generated/Date.h
    ../generated/Date_wrappers.cpp
    ../DateInternal.h
    ../Date.cpp

    ../generated/Dialog.h
    ../generated/Dialog_wrappers.cpp
    ../Dialog.cpp

    ../generated/DockWidget.h
    ../generated/DockWidget_wrappers.cpp
    ../DockWidget.cpp

    ../generated/Enums.h
    ../generated/Enums_wrappers.cpp
    ../Enums.cpp

    ../generated/FileDialog.h
    ../generated/FileDialog_wrappers.cpp
    ../FileDialog.cpp

    ../generated/Frame.h
    ../generated/Frame_wrappers.cpp
    ../Frame.cpp

    ../generated/GridLayout.h
    ../generated/GridLayout_wrappers.cpp
    ../GridLayout.cpp

    ../generated/GroupBox.h
    ../generated/GroupBox_wrappers.cpp
    ../GroupBox.cpp

    ../generated/Icon.h
    ../generated/Icon_wrappers.cpp
    ../IconInternal.h                           # needed for internal struct definitions (when accessing icons from other modules)
    ../Icon.cpp

    ../generated/KeySequence.h
    ../generated/KeySequence_wrappers.cpp
    ../KeySequenceInternal.h
    ../KeySequence.cpp

    ../generated/Label.h
    ../generated/Label_wrappers.cpp
    ../Label.cpp

    ../generated/Layout.h
    ../generated/Layout_wrappers.cpp
    ../Layout.cpp

    ../generated/LineEdit.h
    ../generated/LineEdit_wrappers.cpp
    ../LineEdit.cpp

    ../generated/ListView.h
    ../generated/ListView_wrappers.cpp
    ../ListView.cpp

    ../generated/MainWindow.h
    ../generated/MainWindow_wrappers.cpp
    ../MainWindow.cpp

    ../generated/Menu.h
    ../generated/Menu_wrappers.cpp
    ../Menu.cpp

    ../generated/MenuBar.h
    ../generated/MenuBar_wrappers.cpp
    ../MenuBar.cpp

    ../generated/MessageBox.h
    ../generated/MessageBox_wrappers.cpp
    ../MessageBox.cpp

    ../generated/ModelIndex.h
    ../generated/ModelIndex_wrappers.cpp
    ../ModelIndexInternal.h
    ../ModelIndex.cpp

    ../generated/Object.h
    ../generated/Object_wrappers.cpp
    ../Object.cpp

    ../generated/PaintDevice.h
    ../generated/PaintDevice_wrappers.cpp
    ../PaintDevice.cpp

    ../generated/Painter.h
    ../generated/Painter_wrappers.cpp
    ../Painter.cpp

    ../generated/PaintResources.h
    ../generated/PaintResources_wrappers.cpp
    ../PaintResourcesInternal.h                      # needed for internal struct definitions
    ../PaintResources.cpp

    ../generated/PersistentModelIndex.h
    ../generated/PersistentModelIndex_wrappers.cpp
    ../PersistentModelIndex.cpp

    ../generated/Pixmap.h
    ../generated/Pixmap_wrappers.cpp
    ../PixmapInternal.h
    ../Pixmap.cpp

    ../generated/PlainTextEdit.h
    ../generated/PlainTextEdit_wrappers.cpp
    ../PlainTextEdit.cpp

    ../generated/ProgressBar.h
    ../generated/ProgressBar_wrappers.cpp
    ../ProgressBar.cpp

    ../generated/PushButton.h
    ../generated/PushButton_wrappers.cpp
    ../PushButton.cpp

    ../generated/RadioButton.h
    ../generated/RadioButton_wrappers.cpp
    ../RadioButton.cpp

    ../generated/Region.h
    ../generated/Region_wrappers.cpp
    ../RegionInternal.h
    ../Region.cpp

    ../generated/RegularExpression.h
    ../generated/RegularExpression_wrappers.cpp
    ../RegularExpressionInternal.h
    ../RegularExpression.cpp

    ../generated/ScrollArea.h
    ../generated/ScrollArea_wrappers.cpp
    ../ScrollArea.cpp

    ../generated/SizePolicy.h
    ../generated/SizePolicy_wrappers.cpp
    ../SizePolicyInternal.h
    ../SizePolicy.cpp

    ../generated/Slider.h
    ../generated/Slider_wrappers.cpp
    ../Slider.cpp

    ../generated/SortFilterProxyModel.h
    ../generated/SortFilterProxyModel_wrappers.cpp
    ../SortFilterProxyModel.cpp

    ../generated/StatusBar.h
    ../generated/StatusBar_wrappers.cpp
    ../StatusBar.cpp

    ../generated/StyledItemDelegate.h
    ../generated/StyledItemDelegate_wrappers.cpp
    ../StyledItemDelegate.cpp

    ../generated/StyleOption.h
    ../generated/StyleOption_wrappers.cpp
    ../StyleOption.cpp

    ../generated/StyleOptionViewItem.h
    ../generated/StyleOptionViewItem_wrappers.cpp
    ../StyleOptionViewItem.cpp

    ../generated/TabBar.h
    ../generated/TabBar_wrappers.cpp
    ../TabBar.cpp

    ../generated/TabWidget.h
    ../generated/TabWidget_wrappers.cpp
    ../TabWidget.cpp

    ../generated/TextOption.h
    ../generated/TextOption_wrappers.cpp
    ../TextOption.cpp

    ../generated/Timer.h
    ../generated/Timer_wrappers.cpp
    ../Timer.cpp

    ../generated/ToolBar.h
    ../generated/ToolBar_wrappers.cpp
    ../ToolBar.cpp

    ../generated/TreeView.h
    ../generated/TreeView_wrappers.cpp
    ../TreeView.cpp

    ../generated/Variant.h
    ../generated/Variant_wrappers.cpp
    ../VariantInternal.h
    ../Variant.cpp

    ../generated/Widget.h
    ../generated/Widget_wrappers.cpp
    ../Widget.cpp

    ../util/convert.h
    ../util/convert.cpp
    ../util/SignalStuff.h
)

target_link_libraries(MinimalQtForFSharpImpl PRIVATE Qt${QT_VERSION_MAJOR}::Widgets)

target_compile_definitions(MinimalQtForFSharpImpl PRIVATE QTTESTINGIMPL_LIBRARY)
