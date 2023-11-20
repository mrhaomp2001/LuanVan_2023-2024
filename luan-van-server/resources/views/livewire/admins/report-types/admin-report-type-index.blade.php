<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-2 mb-3">
            <h2>
                Quản lý các lý do báo cáo
            </h2>
        </div>
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Lý do báo báo bài viết</h3>
                <a class="btn btn-outline-primary" href="{{ route('admin.report-type.create', ['model_type' => 'post']) }}">+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($posts as $report)
                            <tr>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->name }}</td>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->description }}</td>
                                <td>
                                    @if ($report->can_use)
                                        <div class="text-success">
                                            Đang được dùng
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center">
                                    <a class="btn btn-sm btn-primary" href="{{ route('admin.report-type.show', ['report_type_id' => $report->id]) }}">
                                        sửa
                                    </a>
                                </td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $posts->links() }}
            </div>
        </div>

        <div class="bg-secondary text-center rounded p-4 mt-3">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Lý do báo báo bình luận</h3>
                <a class="btn btn-outline-primary" href="{{ route('admin.report-type.create', ['model_type' => 'comment']) }}">+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($comments as $report)
                            <tr>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->name }}</td>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->description }}</td>
                                <td>
                                    @if ($report->can_use)
                                        <div class="text-success">
                                            Đang được dùng
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center">
                                    <a class="btn btn-sm btn-primary" href="{{ route('admin.report-type.show', ['report_type_id' => $report->id]) }}">
                                        sửa
                                    </a>
                                </td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $comments->links() }}
            </div>
        </div>


        <div class="bg-secondary text-center rounded p-4 mt-3">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Lý do báo báo bài thảo luận</h3>
                <a class="btn btn-outline-primary" href="{{ route('admin.report-type.create', ['model_type' => 'topic']) }}">+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($topics as $report)
                            <tr>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->name }}</td>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->description }}</td>
                                <td>
                                    @if ($report->can_use)
                                        <div class="text-success">
                                            Đang được dùng
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center">
                                    <a class="btn btn-sm btn-primary" href="{{ route('admin.report-type.show', ['report_type_id' => $report->id]) }}">
                                        sửa
                                    </a>
                                </td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $topics->links() }}
            </div>


        </div>

        <div class="bg-secondary text-center rounded p-4 mt-3">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Lý do báo báo bình luận của bài thảo luận</h3>
                <a class="btn btn-outline-primary" href="{{ route('admin.report-type.create', ['model_type' => 'topic_comment']) }}">+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($topic_comments as $report)
                            <tr>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->name }}</td>
                                <td class="text-truncate" style="min-width: 350px; max-width: 350px;">{{ $report->description }}</td>
                                <td>
                                    @if ($report->can_use)
                                        <div class="text-success">
                                            Đang được dùng
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center">
                                    <a class="btn btn-sm btn-primary" href="{{ route('admin.report-type.show', ['report_type_id' => $report->id]) }}">
                                        sửa
                                    </a>
                                </td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $topic_comments->links() }}
            </div>


        </div>
    </div>


</div>
