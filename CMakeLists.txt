cmake_minimum_required(VERSION 2.6)
project(Bootcamp)

# Define source files
set(source_dir ${PROJECT_SOURCE_DIR}/src)
set(sources
	${source_dir}/Game.cpp
	${source_dir}/GameController.cpp
	${source_dir}/PlatformController.cpp
	${source_dir}/CategoryController.cpp
	${source_dir}/commandhandler.cpp
	${source_dir}/launcher.cpp
	${source_dir}/listfilter.cpp
	${source_dir}/UIManager.cpp
)

set(tests_dir ${PROJECT_SOURCE_DIR}/tests)
set(test_sources
	${tests_dir}/main.cpp
	${tests_dir}/GameTests.cpp
	${tests_dir}/GameControllerTests.cpp
	${tests_dir}/PlatformControllerTests.cpp
	${tests_dir}/CategoryControllerTests.cpp
	${tests_dir}/CommandHandlerTests.cpp
	${tests_dir}/ListFilterTests.cpp
)

# PDCurses
set(CURSES_DIR ${PROJECT_SOURCE_DIR}/dependencies/pdcurses)
include_directories(${CURSES_DIR}/include)
link_directories(${CURSES_DIR}/lib)

# Build and run unit tests
set(GMOCK_DIR ${PROJECT_SOURCE_DIR}/dependencies/gmock)
set(GTEST_DIR ${PROJECT_SOURCE_DIR}/dependencies/gmock/gtest)
set(GMOCK_INCLUDE_DIR ${GMOCK_DIR}/include)
set(GTEST_INCLUDE_DIR ${GTEST_DIR}/include)
include_directories(${GMOCK_DIR} ${GTEST_DIR} ${GMOCK_INCLUDE_DIR} ${GTEST_INCLUDE_DIR} ${PROJECT_SOURCE_DIR}/../src)
link_directories(${PROJECT_SOURCE_DIR}/dependencies/gflags-2.0/.libs)
add_executable(tests ${test_sources} ${sources} ${GMOCK_DIR}/src/gmock-all.cc ${GMOCK_DIR}/gtest/src/gtest-all.cc)
add_custom_command(TARGET tests POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy_if_different "${CURSES_DIR}/bin/curses2.dll" "${CMAKE_CURRENT_BINARY_DIR}/curses2.dll")
add_custom_command(TARGET tests POST_BUILD COMMAND tests)
target_link_libraries(tests pdcurses.lib libgflags.a)

# Build core library
set(CMAKE_EXE_LINKER_FLAGS "-static-libgcc -static-libstdc++")
include_directories(${PROJECT_SOURCE_DIR}/src)
include_directories(${PROJECT_SOURCE_DIR}/dependencies/gflags-2.0/src)
link_directories(${PROJECT_SOURCE_DIR}/dependencies/gflags-2.0/.libs)
add_executable(core ${source_dir}/main.cpp ${sources})
add_custom_command(TARGET core POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy_if_different "${CURSES_DIR}/bin/curses2.dll" "${CMAKE_CURRENT_BINARY_DIR}/curses2.dll")
target_link_libraries(core pdcurses.lib libgflags.a)
