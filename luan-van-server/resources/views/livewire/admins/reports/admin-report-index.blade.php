<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-2 mb-3">
            <div class="d-flex align-items-center justify-content-between">
                <h3 class="mb-0">
                    Quản lý báo cáo (có tất cả {{ count($posts) + count($comments) + count($topics) + count($topic_comments) }} báo cáo mới)</h3>
                <a class="btn btn-outline-info" href="{{ route("admin.report.history") }}"> Lịch sử xử lý</a>
            </div>
        </div>
        <div class="row bg-secondary rounded justify-content-center mx-0 my-3">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <div class="table-responsive">
                            <div>
                                <h3 class="text-start">Bài viết bị báo cáo ({{ count($posts) }} báo cáo mới)</h3>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">ID</th>
                                            <th scope="col">Người báo cáo</th>
                                            <th scope="col">lý do báo cáo</th>
                                            <th scope="col">nội dung báo cáo</th>
                                            <th scope="col">Xem trước</th>
                                            <th scope="col">Chi tiết</th>
                                        </tr>
                                    </thead>
                                    @foreach ($posts as $report)
                                        <tbody>
                                            <tr>
                                                <th class="align-middle" scope="row">{{ $report->id }}</th>
                                                <td class="align-middle">{{ $report->user->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->reportType->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->content }}
                                                </td>
                                                <td class="align-middle">
                                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal{{ $report->id }}">
                                                        Xem trước
                                                    </button>
                                                </td>
                                                <td class="align-middle">
                                                    <a href="{{ route('admin.report.show', ['report_id' => $report->id]) }}" class="btn btn-info">Duyệt qua</a>
                                                </td>
                                            </tr>
                                        </tbody>

                                        <div class="modal fade" id="modal{{ $report->id }}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-scrollable">
                                                <div class="modal-content">
                                                    {{-- <div class="modal-header">
                                                        <h1 class="modal-title fs-5 text-light" id="staticBackdropLabel">
                                                            Bài viết của {{ $report->user->name }}
                                                        </h1>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div> --}}
                                                    <div class="modal-body">

                                                        <p class="text-dark text-start">
                                                            Bài viết của {{ $report->user->name }}
                                                        </p>

                                                        @if ($report->model->title != '')
                                                            <h5 class="text-dark text-start">
                                                                Tiêu đề: {{ $report->model->title }}
                                                            </h5>
                                                        @endif

                                                        <p class="text-dark text-start">
                                                            Nội dung: {{ $report->model->content }}
                                                        </p>

                                                        <div>
                                                            @if ($report->model->image_path != '')
                                                                <img src="{{ $report->model->image_path }}"class="img-fluid" style="max-height: 250px;">
                                                            @endif
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Thoát ra</button>
                                                        {{-- <button type="button" class="btn btn-primary">Understood</button> --}}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    @endforeach
                                </table>
                            </div>
                            {{ $posts->links(data: ['scrollTo' => false]) }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">

                    <div class="bg-secondary rounded p-4 ">
                        <div class="table-responsive">
                            <div>
                                <h3 class="text-start">Bình luận bị báo cáo ({{ count($comments) }} báo cáo mới)</h3>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">ID</th>
                                            <th scope="col">Người báo cáo</th>
                                            <th scope="col">lý do báo cáo</th>
                                            <th scope="col">nội dung báo cáo</th>
                                            <th scope="col">Xem trước</th>
                                            <th scope="col">Chi tiết</th>
                                        </tr>
                                    </thead>
                                    @foreach ($comments as $report)
                                        <tbody>
                                            <tr>
                                                <th class="align-middle" scope="row">{{ $report->id }}</th>
                                                <td class="align-middle">{{ $report->user->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->reportType->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->content }}
                                                </td>
                                                <td class="align-middle">
                                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal{{ $report->id }}">
                                                        Xem trước
                                                    </button>
                                                </td>
                                                <td class="align-middle">
                                                    <a href="{{ route('admin.report.show', ['report_id' => $report->id]) }}" class="btn btn-info">Duyệt qua</a>
                                                </td>
                                            </tr>
                                        </tbody>

                                        <div class="modal fade" id="modal{{ $report->id }}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-scrollable">
                                                <div class="modal-content">
                                                    {{-- <div class="modal-header">
                                                        <h1 class="modal-title fs-5 text-light" id="staticBackdropLabel">
                                                            Bài viết của {{ $report->user->name }}
                                                        </h1>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div> --}}
                                                    <div class="modal-body">

                                                        <p class="text-dark text-start">
                                                            Bài viết của {{ $report->user->name }}
                                                        </p>

                                                        @if ($report->model->title != '')
                                                            <h5 class="text-dark text-start">
                                                                Tiêu đề: {{ $report->model->title }}
                                                            </h5>
                                                        @endif

                                                        <p class="text-dark text-start">
                                                            Nội dung: {{ $report->model->content }}
                                                        </p>

                                                        <div>
                                                            @if ($report->model->image_path != '')
                                                                <img src="{{ $report->model->image_path }}"class="img-fluid" style="max-height: 250px;">
                                                            @endif
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Thoát ra</button>
                                                        {{-- <button type="button" class="btn btn-primary">Understood</button> --}}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    @endforeach
                                </table>
                            </div>
                            {{ $comments->links(data: ['scrollTo' => false]) }}
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row bg-secondary rounded justify-content-center mx-0 my-3">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <div class="table-responsive">
                            <div>
                                <h3 class="text-start">Bài thảo luận bị báo cáo ({{ count($topics) }} báo cáo mới)</h3>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">ID</th>
                                            <th scope="col">Người báo cáo</th>
                                            <th scope="col">lý do báo cáo</th>
                                            <th scope="col">nội dung báo cáo</th>
                                            <th scope="col">Xem trước</th>
                                            <th scope="col">Chi tiết</th>
                                        </tr>
                                    </thead>
                                    @foreach ($topics as $report)
                                        <tbody>
                                            <tr>
                                                <th class="align-middle" scope="row">{{ $report->id }}</th>
                                                <td class="align-middle">{{ $report->user->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->reportType->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->content }}
                                                </td>
                                                <td class="align-middle">
                                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal{{ $report->id }}">
                                                        Xem trước
                                                    </button>
                                                </td>
                                                <td class="align-middle">
                                                    <a href="{{ route('admin.report.show', ['report_id' => $report->id]) }}" class="btn btn-info">Duyệt qua</a>
                                                </td>
                                            </tr>
                                        </tbody>

                                        <div class="modal fade" id="modal{{ $report->id }}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-scrollable">
                                                <div class="modal-content">
                                                    {{-- <div class="modal-header">
                                                        <h1 class="modal-title fs-5 text-light" id="staticBackdropLabel">
                                                            Bài viết của {{ $report->user->name }}
                                                        </h1>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div> --}}
                                                    <div class="modal-body">

                                                        <p class="text-dark text-start">
                                                            Bình luận của {{ $report->user->name }}
                                                        </p>

                                                        @if ($report->model->title != '')
                                                            <h5 class="text-dark text-start">
                                                                Tiêu đề: {{ $report->model->title }}
                                                            </h5>
                                                        @endif

                                                        <p class="text-dark text-start">
                                                            Nội dung: {{ $report->model->content }}
                                                        </p>

                                                        <div>
                                                            @if ($report->model->image_path != '')
                                                                <img src="{{ $report->model->image_path }}"class="img-fluid" style="max-height: 250px;">
                                                            @endif
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Thoát ra</button>
                                                        {{-- <button type="button" class="btn btn-primary">Understood</button> --}}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    @endforeach
                                </table>
                            </div>
                            {{ $topics->links(data: ['scrollTo' => false]) }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <div class="table-responsive">
                            <div>
                                <h3 class="text-start">Bình luận trong thảo luận bị báo cáo ({{ count($topic_comments) }} báo cáo mới)</h3>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">ID</th>
                                            <th scope="col">Người báo cáo</th>
                                            <th scope="col">lý do báo cáo</th>
                                            <th scope="col">nội dung báo cáo</th>
                                            <th scope="col">Xem trước</th>
                                            <th scope="col">Chi tiết</th>
                                        </tr>
                                    </thead>
                                    @foreach ($topic_comments as $report)
                                        <tbody>
                                            <tr>
                                                <th class="align-middle" scope="row">{{ $report->id }}</th>
                                                <td class="align-middle">{{ $report->user->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->reportType->name }}</td>
                                                <td class="align-middle w-25" style="white-space: pre-line">{{ $report->content }}
                                                </td>
                                                <td class="align-middle">
                                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal{{ $report->id }}">
                                                        Xem trước
                                                    </button>
                                                </td>
                                                <td class="align-middle">
                                                    <a href="{{ route('admin.report.show', ['report_id' => $report->id]) }}" class="btn btn-info">Duyệt qua</a>
                                                </td>
                                            </tr>
                                        </tbody>

                                        <div class="modal fade" id="modal{{ $report->id }}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-scrollable">
                                                <div class="modal-content">
                                                    {{-- <div class="modal-header">
                                                        <h1 class="modal-title fs-5 text-light" id="staticBackdropLabel">
                                                            Bài viết của {{ $report->user->name }}
                                                        </h1>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div> --}}
                                                    <div class="modal-body">

                                                        <p class="text-dark text-start">
                                                            Bình luận của {{ $report->user->name }}
                                                        </p>

                                                        @if ($report->model->title != '')
                                                            <h5 class="text-dark text-start">
                                                                Tiêu đề: {{ $report->model->title }}
                                                            </h5>
                                                        @endif

                                                        <p class="text-dark text-start">
                                                            Nội dung: {{ $report->model->content }}
                                                        </p>

                                                        <div>
                                                            @if ($report->model->image_path != '')
                                                                <img src="{{ $report->model->image_path }}"class="img-fluid" style="max-height: 250px;">
                                                            @endif
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Thoát ra</button>
                                                        {{-- <button type="button" class="btn btn-primary">Understood</button> --}}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    @endforeach
                                </table>
                            </div>
                            {{ $topic_comments->links(data: ['scrollTo' => false]) }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
