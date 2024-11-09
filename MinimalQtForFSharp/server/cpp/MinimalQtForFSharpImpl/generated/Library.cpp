#include "Application_wrappers.h"
#include "Common_wrappers.h"
#include "Object_wrappers.h"
#include "Layout_wrappers.h"
#include "Color_wrappers.h"
#include "PaintResources_wrappers.h"
#include "Enums_wrappers.h"
#include "Painter_wrappers.h"
#include "Icon_wrappers.h"
#include "KeySequence_wrappers.h"
#include "Action_wrappers.h"
#include "Region_wrappers.h"
#include "Cursor_wrappers.h"
#include "SizePolicy_wrappers.h"
#include "Widget_wrappers.h"
#include "BoxLayout_wrappers.h"
#include "Date_wrappers.h"
#include "CalendarWidget_wrappers.h"
#include "Variant_wrappers.h"
#include "ModelIndex_wrappers.h"
#include "PersistentModelIndex_wrappers.h"
#include "AbstractItemModel_wrappers.h"
#include "ComboBox_wrappers.h"
#include "Dialog_wrappers.h"
#include "FileDialog_wrappers.h"
#include "GridLayout_wrappers.h"
#include "GroupBox_wrappers.h"
#include "Frame_wrappers.h"
#include "Pixmap_wrappers.h"
#include "Label_wrappers.h"
#include "LineEdit_wrappers.h"
#include "AbstractListModel_wrappers.h"
#include "AbstractScrollArea_wrappers.h"
#include "AbstractItemDelegate_wrappers.h"
#include "AbstractItemView_wrappers.h"
#include "ListView_wrappers.h"
#include "Menu_wrappers.h"
#include "MenuBar_wrappers.h"
#include "DockWidget_wrappers.h"
#include "ToolBar_wrappers.h"
#include "StatusBar_wrappers.h"
#include "TabWidget_wrappers.h"
#include "MainWindow_wrappers.h"
#include "AbstractButton_wrappers.h"
#include "MessageBox_wrappers.h"
#include "TextOption_wrappers.h"
#include "PlainTextEdit_wrappers.h"
#include "ProgressBar_wrappers.h"
#include "PushButton_wrappers.h"
#include "RadioButton_wrappers.h"
#include "ScrollArea_wrappers.h"
#include "AbstractSlider_wrappers.h"
#include "Slider_wrappers.h"
#include "AbstractProxyModel_wrappers.h"
#include "RegularExpression_wrappers.h"
#include "SortFilterProxyModel_wrappers.h"
#include "StyleOption_wrappers.h"
#include "StyleOptionViewItem_wrappers.h"
#include "StyledItemDelegate_wrappers.h"
#include "TabBar_wrappers.h"
#include "Timer_wrappers.h"
#include "TreeView_wrappers.h"

extern "C" int nativeLibraryInit() {
    ::Application::__register();
    ::Common::__register();
    ::Object::__register();
    ::Layout::__register();
    ::Color::__register();
    ::PaintResources::__register();
    ::Enums::__register();
    ::Painter::__register();
    ::Icon::__register();
    ::KeySequence::__register();
    ::Action::__register();
    ::Region::__register();
    ::Cursor::__register();
    ::SizePolicy::__register();
    ::Widget::__register();
    ::BoxLayout::__register();
    ::Date::__register();
    ::CalendarWidget::__register();
    ::Variant::__register();
    ::ModelIndex::__register();
    ::PersistentModelIndex::__register();
    ::AbstractItemModel::__register();
    ::ComboBox::__register();
    ::Dialog::__register();
    ::FileDialog::__register();
    ::GridLayout::__register();
    ::GroupBox::__register();
    ::Frame::__register();
    ::Pixmap::__register();
    ::Label::__register();
    ::LineEdit::__register();
    ::AbstractListModel::__register();
    ::AbstractScrollArea::__register();
    ::AbstractItemDelegate::__register();
    ::AbstractItemView::__register();
    ::ListView::__register();
    ::Menu::__register();
    ::MenuBar::__register();
    ::DockWidget::__register();
    ::ToolBar::__register();
    ::StatusBar::__register();
    ::TabWidget::__register();
    ::MainWindow::__register();
    ::AbstractButton::__register();
    ::MessageBox::__register();
    ::TextOption::__register();
    ::PlainTextEdit::__register();
    ::ProgressBar::__register();
    ::PushButton::__register();
    ::RadioButton::__register();
    ::ScrollArea::__register();
    ::AbstractSlider::__register();
    ::Slider::__register();
    ::AbstractProxyModel::__register();
    ::RegularExpression::__register();
    ::SortFilterProxyModel::__register();
    ::StyleOption::__register();
    ::StyleOptionViewItem::__register();
    ::StyledItemDelegate::__register();
    ::TabBar::__register();
    ::Timer::__register();
    ::TreeView::__register();
    // should we do module inits here as well?
    // currently they are manually done on the C# side inside the <module>.Init methods (which perform registration first) - and those are individually called by Library.Init, which first calls nativeImplInit
    return 0;
}

extern "C" void nativeLibraryShutdown() {
    // module shutdowns? see note above
}
