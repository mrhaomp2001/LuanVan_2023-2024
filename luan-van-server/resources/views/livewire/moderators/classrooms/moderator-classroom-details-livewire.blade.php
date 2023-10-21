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


    <div class="container-fluid px-4">
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
                <h6 class="mb-0">Tài liệu học tập</h6>
                <a class="btn btn-outline-primary" href="{{ route('moderator.documents.create', ['classroom_id' => $classroom->id]) }}" wire:navigate>+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Trang</th>
                            <th scope="col">Nội dung</th>
                            <th scope="col">Xem trước</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($documents as $document)
                            <tr>
                                <td style="max-width: 20px;">{{ $document->page }}</td>
                                <td class="text-truncate" style="max-width: 400px;">{{ $document->content }}</td>
                                <td class="text-center" style="max-width: 50px;"><button type="button" class="btn btn-sm btn-outline-info" data-bs-toggle="modal" data-bs-target="#modal{{ $document->id }}">
                                        Xem trước
                                    </button></td>
                                <td><a class="btn btn-sm btn-primary" href="{{ route("moderator.documents.edit", ["classroom_id"=>$classroom->id, "study_document_id" => $document->id]) }}">Sửa</a></td>
                            </tr>

                            <div class="modal fade" id="modal{{ $document->id }}" tabindex="-1" aria-labelledby="modal{{ $document->id }}" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered ">
                                    <div class="modal-content bg-secondary border border-2 border-light rounded">
                                        <div class="modal-body">
                                            <div class="text-start mt-0 pt-0 pb-3" style="white-space: pre-line">
                                                {{ $document->content }}
                                            </div>
                                            <div>
                                                <img src="{{ $document->image_path }}" class="img-fluid img-thumbnail" alt="" >
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-outline-primary" data-bs-dismiss="modal">Được</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $documents->links() }}
            </div>
        </div>
    </div>

</div>
