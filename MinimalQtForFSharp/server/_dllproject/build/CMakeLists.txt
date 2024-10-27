cmake_minimum_required(VERSION 3.14)

if(WIN32)
    set(CMAKE_PREFIX_PATH "C:/Qt/6.8.0/msvc2022_64/lib/cmake") # binaries from Qt installer
elseif(LINUX)
    set(CMAKE_PREFIX_PATH "~/Qt/lib/cmake")                # self-compiled from Qt source (comment out if using system version - but be certain system version is 6.7.2 or greater!)
#    set(CMAKE_PREFIX_PATH "~/Qt/6.7.2/gcc_64/lib/cmake")  # binaries from Qt installer - note: NOT WORKING
elseif(APPLE)
    set(CMAKE_PREFIX_PATH "~/Qt/6.7.2/macos/lib/cmake")    # binaries from Qt installer (comment out if using homebrew version)
#    set(CMAKE_OSX_ARCHITECTURES "x86_64;arm64")
endif()

project(MinimalQtForFSharpServer LANGUAGES CXX)

set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_VISIBILITY_PRESET hidden)

find_package(QT NAMES Qt6 Qt5 REQUIRED COMPONENTS Widgets)
find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS Widgets)

if (WIN32)
    add_library(MinimalQtForFSharpServer SHARED
            ../dllmain.cpp
            ../core/NativeImplCore.cpp
            ../DirectExport.h
            ../DirectExport.cpp
    )
else()
    add_library(MinimalQtForFSharpServer SHARED
            ../core/NativeImplCore.cpp
            ../DirectExport.h
            ../DirectExport.cpp
    )
endif()

add_subdirectory("../../cpp/MinimalQtForFSharpImpl/build" "${CMAKE_CURRENT_BINARY_DIR}/MinimalQtForFSharpImpl")

target_link_libraries(MinimalQtForFSharpServer PRIVATE Qt${QT_VERSION_MAJOR}::Widgets "MinimalQtForFSharpImpl")

target_compile_definitions(MinimalQtForFSharpServer PRIVATE QTTESTINGSERVER_LIBRARY)

#if (WIN32 AND NOT DEFINED CMAKE_TOOLCHAIN_FILE)
#    set(DEBUG_SUFFIX)
#    if (MSVC AND CMAKE_BUILD_TYPE MATCHES "Debug")
#        set(DEBUG_SUFFIX "d")
#    endif ()
#    set(QT_INSTALL_PATH "${CMAKE_PREFIX_PATH}")
#    if (NOT EXISTS "${QT_INSTALL_PATH}/bin")
#        set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
#        if (NOT EXISTS "${QT_INSTALL_PATH}/bin")
#            set(QT_INSTALL_PATH "${QT_INSTALL_PATH}/..")
#        endif ()
#    endif ()
#    if (EXISTS "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll")
#        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
#                COMMAND ${CMAKE_COMMAND} -E make_directory
#                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/")
#        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
#                COMMAND ${CMAKE_COMMAND} -E copy
#                "${QT_INSTALL_PATH}/plugins/platforms/qwindows${DEBUG_SUFFIX}.dll"
#                "$<TARGET_FILE_DIR:${PROJECT_NAME}>/plugins/platforms/")
#    endif ()
#    foreach (QT_LIB Core Gui Widgets)
#        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
#                COMMAND ${CMAKE_COMMAND} -E copy
#                "${QT_INSTALL_PATH}/bin/Qt6${QT_LIB}${DEBUG_SUFFIX}.dll"
#                "$<TARGET_FILE_DIR:${PROJECT_NAME}>")
#    endforeach (QT_LIB)
#endif ()
