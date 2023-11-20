@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <h2 class="pt-3">Các chức năng của bạn</h2>
                    <div class="bg-secondary rounded p-2 ">
                        @if (Auth::check())
                            @if (auth()->user()->role->id >= 3)
                                <hr class="my-1" />
                                <div class="mx-2">
                                    Quản trị hệ thống
                                </div>
                                <a href="{{ route('classrooms.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Tất cả lớp học</a>
                                <a href="{{ route('admin.report.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-book me-2 "></i>Các báo cáo</a>
                                <a href="{{ route('admin.game.index') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-gamepad me-2"></i>Trò chơi</a>
                                <a href="{{ route('admin.moderators.show') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-user-tie me-2"></i>Quản trị viên</a>
                                <a href="{{ route('admin.user.index') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-user me-2"></i>Người dùng</a>
                                <a href="{{ route('admin.template.index') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-table-list me-2"></i>Mẫu bài viết</a>
                                <a href="{{ route('admin.report-type.index') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-flag-checkered me-2"></i>Lý do báo cáo</a>
                                <a href="{{ route('admin.system-notification.index') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-bullhorn me-2"></i>Thông báo</a>
                                {{-- <a href="{{ route('admin.moderators.show') }}" class="nav-item nav-link mx-2"><i class="fa-solid fa-bullhorn me-2"></i>Thông báo</a> --}}
                            @endif
                            @if (auth()->user()->role->id >= 2)
                                <hr class="my-1" />
                                <div class="mx-2">
                                    Quản trị nội dung
                                </div>
                                <a href="{{ route('moderator.classrooms.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Lớp học của bạn</a>
                            @endif
                        @else
                            <a href="{{ route('login') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Đăng nhập</a>
                            {{-- <a href="{{ route('register') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Đăng ký</a> --}}
                        @endif

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Blank End -->
@stop
