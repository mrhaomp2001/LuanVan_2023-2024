<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <title>DarkPan - Bootstrap 5 Admin Template</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="" name="keywords">
    <meta content="" name="description">

    <!-- Favicon -->
    <link href="img/favicon.ico" rel="icon">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&family=Roboto:wght@500;700&display=swap" rel="stylesheet">

    <!-- Icon Font Stylesheet -->
    {{-- <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet"> --}}
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" rel="stylesheet">
    

    <!-- Libraries Stylesheet -->
    <link href="{{ asset('lib/owlcarousel/assets/owl.carousel.min.css') }}" rel="stylesheet">
    <link href="{{ asset('lib/tempusdominus/css/tempusdominus-bootstrap-4.min.css') }}" rel="stylesheet" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="{{ asset('css/bootstrap.min.css') }}" rel="stylesheet">

    <!-- Template Stylesheet -->
    <link href="{{ asset('css/style.css') }}" rel="stylesheet">
</head>

<body>
    <div class="container-fluid position-relative d-flex p-0">
        {{-- <!-- Spinner Start -->
        <div id="spinner" class="show bg-dark position-fixed translate-middle w-100 vh-100 top-50 start-50 d-flex align-items-center justify-content-center">
            <div class="spinner-border text-primary" style="width: 3rem; height: 3rem;" role="status">
                <span class="sr-only">Đang tải...</span>
            </div>
        </div>
        <!-- Spinner End --> --}}


        <!-- Sidebar Start -->
        <div class="sidebar pb-3">
            <nav class="navbar bg-secondary navbar-dark">
                <a href="{{ route('dashboard') }}" class="navbar-brand mx-4 mb-3">
                    <h3 class="text-primary"><i class="fa fa-user-edit me-2"></i>Hội học thuật</h3>
                </a>
                @if (Auth::check())
                    <div class="d-flex align-items-center ms-4 mb-4">
                        <div class="position-relative">
                            @if (auth()->user()->avatar_path != '')
                                <img class="rounded-circle" src="{{ auth()->user()->avatar_path }}" alt="" style="width: 40px; height: 40px;">
                            @else
                                <img class="rounded-circle" src="{{ Storage::url('users/avatars/0.png') }}" alt="" style="width: 40px; height: 40px;">
                            @endif
                            <div class="bg-success rounded-circle border border-2 border-white position-absolute end-0 bottom-0 p-1">
                            </div>
                        </div>
                        <div class="ms-3">
                            <h6 class="mb-0">{{ auth()->user()->name }}</h6>
                            <span>{{ auth()->user()->role->description }}</span>
                        </div>
                    </div>
                @endif

                <div class="navbar-nav w-100">
                    @if (Auth::check())
                        @if (auth()->user()->role->id >= 3)
                            <hr class="my-1" />
                            <div class="mx-2">
                                Quản trị hệ thống
                            </div>
                            <a href="{{ route('classrooms.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Tất cả lớp học</a>
                            <a href="{{ route('reports.posts.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-book me-2 "></i>Các báo cáo</a>
                        @endif
                        @if (auth()->user()->role->id >= 2)
                            <hr class="my-1" />
                            <div class="mx-2">
                                Quản trị nội dung
                            </div>
                            <a href="{{ route('moderator.classrooms.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Lớp học của bạn</a>
                            <a href="{{ route('reports.posts.index') }}" class="nav-item nav-link mx-2"><i class="fa fa-book me-2 "></i>Các báo cáo</a>
                        @endif
                    @else
                        <a href="{{ route('login') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Đăng nhập</a>
                        <a href="{{ route('register') }}" class="nav-item nav-link mx-2"><i class="fa fa-home me-2 "></i>Đăng ký</a>
                    @endif

                </div>


            </nav>
        </div>
        <!-- Sidebar End -->


        <!-- Content Start -->
        <div class="content">
            <!-- Navbar Start -->
            <nav class="navbar navbar-expand bg-secondary navbar-dark sticky-top px-4 py-0">
                <a href="index.html" class="navbar-brand d-flex d-lg-none me-4">
                    <h2 class="text-primary mb-0"><i class="fa fa-user-edit"></i></h2>
                </a>
                <a href="#" class="sidebar-toggler flex-shrink-0">
                    <i class="fa fa-bars"></i>
                </a>
                <form class="mx-4 d-flex">
                    <input class="form-control bg-dark border-0 " type="search" placeholder="Tìm kiếm">
                    <button class="btn btn-primary mx-1">Tìm</button>
                </form>
                <div class="navbar-nav align-items-center ms-auto">
                    <div class="nav-item dropdown">
                        @if (Auth::check())
                            <a href="#" class="nav-link dropdown-toggle" data-bs-toggle="dropdown">

                                {{-- <img class="rounded-circle me-lg-2" src="{{ auth()->user()->avatar_path }}" alt="" style="width: 40px; height: 40px;"> --}}
                                @if (auth()->user()->avatar_path != '')
                                    <img class="rounded-circle" src="{{ auth()->user()->avatar_path }}" alt="" style="width: 40px; height: 40px;">
                                @else
                                    <img class="rounded-circle" src="{{ Storage::url('users/avatars/0.png') }}" alt="" style="width: 40px; height: 40px;">
                                @endif
                                <span class="d-none d-lg-inline-flex">{{ auth()->user()->name }}</span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-end bg-secondary border-0 rounded-0 rounded-bottom m-0">
                                <a href="#" class="dropdown-item">My Profile</a>
                                <a href="#" class="dropdown-item">Settings</a>
                                <form method="POST" action="{{ route('logout') }}">
                                    @csrf
                                    <button type="submit" class="btn text-danger">Đăng xuất</button>
                                </form>
                            </div>
                        @else
                            <a href="{{ route('login') }}">Đăng nhập</a>
                        @endif
                    </div>
                </div>
            </nav>
            <!-- Navbar End -->
            @yield('content')

            {{ $slot }}

            <!-- Footer Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="bg-secondary rounded-top p-4">
                    <div class="row">
                        <div class="col-12 col-sm-6 text-center text-sm-start">
                            &copy; <a href="#">Your Site Name</a>, All Right Reserved.
                        </div>
                        <div class="col-12 col-sm-6 text-center text-sm-end">
                            <!--/*** This template is free as long as you keep the footer author’s credit link/attribution link/backlink. If you'd like to use the template without the footer author’s credit link/attribution link/backlink, you can purchase the Credit Removal License from "https://htmlcodex.com/credit-removal". Thank you for your support. ***/-->
                            Designed By <a href="https://htmlcodex.com">HTML Codex</a>
                            <br>Distributed By: <a href="https://themewagon.com" target="_blank">ThemeWagon</a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Footer End -->
        </div>
        <!-- Content End -->


        <!-- Back to Top -->
        <a href="#" class="btn btn-lg btn-primary btn-lg-square back-to-top"><i class="bi bi-arrow-up"></i></a>
    </div>

    <!-- JavaScript Libraries -->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="{{ asset('lib/chart/chart.min.js') }}"></script>
    <script src="{{ asset('lib/easing/easing.min.js') }}"></script>
    <script src="{{ asset('lib/waypoints/waypoints.min.js') }}"></script>
    <script src="{{ asset('lib/owlcarousel/owl.carousel.min.js') }}"></script>
    <script src="{{ asset('lib/tempusdominus/js/moment.min.js') }}"></script>
    <script src="{{ asset('lib/tempusdominus/js/moment-timezone.min.js') }}"></script>
    <script src="{{ asset('lib/tempusdominus/js/tempusdominus-bootstrap-4.min.js') }}"></script>

    <!-- Template Javascript -->
    <script src="{{ asset('js/main.js') }}"></script>
</body>

</html>
