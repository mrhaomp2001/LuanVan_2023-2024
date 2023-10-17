<div>
    <div class="d-flex justify-content-center my-3">
        <div class="col-sm-12 col-xl-6">
            <div class="bg-secondary rounded h-100 p-4">
                <div class="d-inline-flex align-items-center">

                    <h6 class="mb-4 align-items-center">
                        <a wire:navigate href="{{ route('moderator.classrooms.index') }}" class="btn btn-square btn-primary m-2 align-items-center">
                            <i class="fa fa-arrow-left"></i>
                        </a>
                        Thông tin lớp học
                    </h6>
                </div>
                <dl class="row mb-0">
                    <dt class="col-sm-4">Ảnh đại diện</dt>
                    <dd class="col-sm-8">
                        @if ($image_path != '')
                            <img id="output" class="img-thumbnail" style="max-height: 150px" src="{{ $image_path }}" />
                        @else
                            Chưa có ảnh đại diện
                        @endif
                    </dd>

                    <dt class="col-sm-4">Tên lớp học</dt>
                    <dd class="col-sm-8">{{ $name }}</dd>

                    <dt class="col-sm-4">Miêu tả</dt>
                    <dd class="col-sm-8">{{ $description }}</dd>

                    <dt class="col-sm-4">Màu chủ đề</dt>
                    <dd class="col-sm-8">{{ $theme_color }} (<span style="color: {{ $theme_color }}">{{ $theme_color }}</span>)</dd>

                    <dt class="col-sm-4 text-truncate">Trạng thái</dt>
                    <dd class="col-sm-8">
                        @if ($is_open)
                            <div class="text-success">
                                Đang mở
                            </div>
                        @else
                            <div class="text-danger">
                                Đang đóng
                            </div>
                        @endif
                        </td>
                    </dd>

                    <dt class="col-sm-4">Chỉnh sửa</dt>
                    <dd class="col-sm-8">
                        <a wire:navigate href="{{ route('moderator.classrooms.edit', ['id' => $id]) }}" class="btn btn-outline-info btn-sm"><i class="fa fa-pen me-2"></i>Chỉnh sửa</a>
                    </dd>
                </dl>
            </div>
        </div>
    </div>


    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h6 class="mb-0">Các bộ câu hỏi</h6>
                <a class="btn btn-outline-primary" href="{{ route('moderator.question-collections.create', ['classroom_id' => $id]) }}" wire:navigate>+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white text-center">
                            <th class="w-25" scope="col">Tên</th>
                            <th scope="col">Độ khó</th>
                            <th scope="col">Trò chơi</th>
                            <th scope="col">Số câu hỏi</th>
                            <th scope="col">Mỗi lần chơi</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Chi tiết</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($questionCollections as $questionCollection)
                            <tr>
                                <td class="w-25 text-break" style="max-width: 25%">{{ $questionCollection->name }}</td>
                                <td class="w-25 text-break" style="max-width: 25%">{{ $questionCollection->difficulty }}</td>
                                <td class="text-break" style="max-width: 25%">{{ $questionCollection->game->name }} (Id: {{ $questionCollection->game->id }})</td>
                                <td>{{ count($questionCollection->questions) }}</td>
                                <td>{{ $questionCollection->questions_per_time }} câu</td>
                                <td>
                                    @if ($questionCollection->is_open)
                                        <span class="text-success">đang mở</span>
                                    @else
                                        <span class="text-danger">đã đóng</span>
                                    @endif
                                </td>
                                <td><a wire:navigate href="{{ route('moderator.question-collections.show', ['classroom_id' => $classroom->id, 'question_collection_id' => $questionCollection->id]) }}" class="btn btn-sm btn-primary" href="">Xem</a></td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
        </div>
    </div>


    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h6 class="mb-0">Recent Salse</h6>
                <a href="">Show All</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col"><input class="form-check-input" type="checkbox"></th>
                            <th scope="col">Date</th>
                            <th scope="col">Invoice</th>
                            <th scope="col">Customer</th>
                            <th scope="col">Amount</th>
                            <th scope="col">Status</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><input class="form-check-input" type="checkbox"></td>
                            <td>01 Jan 2045</td>
                            <td>INV-0123</td>
                            <td>Jhon Doe</td>
                            <td>$123</td>
                            <td>Paid</td>
                            <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                        </tr>
                        <tr>
                            <td><input class="form-check-input" type="checkbox"></td>
                            <td>01 Jan 2045</td>
                            <td>INV-0123</td>
                            <td>Jhon Doe</td>
                            <td>$123</td>
                            <td>Paid</td>
                            <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                        </tr>
                        <tr>
                            <td><input class="form-check-input" type="checkbox"></td>
                            <td>01 Jan 2045</td>
                            <td>INV-0123</td>
                            <td>Jhon Doe</td>
                            <td>$123</td>
                            <td>Paid</td>
                            <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                        </tr>
                        <tr>
                            <td><input class="form-check-input" type="checkbox"></td>
                            <td>01 Jan 2045</td>
                            <td>INV-0123</td>
                            <td>Jhon Doe</td>
                            <td>$123</td>
                            <td>Paid</td>
                            <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                        </tr>
                        <tr>
                            <td><input class="form-check-input" type="checkbox"></td>
                            <td>01 Jan 2045</td>
                            <td>INV-0123</td>
                            <td>Jhon Doe</td>
                            <td>$123</td>
                            <td>Paid</td>
                            <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</div>
