<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary rounded mb-3 p-3 d-inline-flex align-items-center">
            <div class="mx-2">
                <a wire:navigate href="{{ route('moderator.classrooms.show', ['id' => $classroom->id]) }}" class="btn btn-square btn-primary ">
                    <i class="fa fa-arrow-left "></i>
                </a>
            </div>
            <div>
                Bạn đang làm việc với bộ câu hỏi <b>{{ $questionCollection->name }}</b> của lớp <b>{{ $classroom->name }}</b>
            </div>
        </div>
    </div>
    <div class="d-flex justify-content-center my-3">
        <div class="col-sm-12 col-xl-6">
            <div class="bg-secondary rounded h-100 p-4">
                <dl class="row mb-0">

                    <dt class="col-sm-4">Tên bộ câu hỏi</dt>
                    <dd class="col-sm-8">{{ $questionCollection->name }}</dd>

                    <dt class="col-sm-4">Miêu tả độ khó</dt>
                    <dd class="col-sm-8">{{ $questionCollection->difficulty }}</dd>

                    <dt class="col-sm-4">Trò chơi</dt>
                    <dd class="col-sm-8">{{ $questionCollection->game->name }}</dd>

                    <dt class="col-sm-4 text-truncate">Trạng thái</dt>
                    <dd class="col-sm-8">
                        @if ($questionCollection->is_open)
                            <div class="text-success">
                                Đang mở
                            </div>
                        @else
                            <div class="text-success">
                                Đã đóng
                            </div>
                        @endif
                        </td>
                    </dd>

                    <dt class="col-sm-4">Chỉnh sửa</dt>
                    <dd class="col-sm-8">
                        <a wire:navigate href="{{ route('moderator.question-collections.edit', ['classroom_id' => $classroom->id, 'question_collection_id' => $questionCollection->id]) }}" class="btn btn-outline-info btn-sm"><i class="fa fa-pen me-2"></i>Chỉnh sửa</a>
                    </dd>
                </dl>
            </div>
        </div>

    </div>
    <div class="d-flex justify-content-center my-3 mx-3">
        <div class="col-sm-12">
            <div class="bg-secondary rounded h-100 p-4">
                <div class="d-flex align-items-center justify-content-between mb-4">
                    <h3 class="mb-0">Các câu hỏi</h3>
                    <a class="btn btn-outline-primary" href="" wire:navigate>+ Thêm câu hỏi</a>
                </div>
                <div class="table-responsive">
                    <table class="table text-start align-middle table-bordered table-hover mb-0">
                        <thead>
                            <tr class="text-white">
                                <th scope="col">Nội dung</th>
                                <th scope="col">Xem trước</th>
                                <th scope="col">Sửa</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach ($questionCollection->questions as $question)
                                <tr>
                                    <td class="text-break" style="max-width: 75%">{{ $question->content }}</td>
                                    <td class="text-center">
                                        <button type="button" class="btn btn-outline-info" data-bs-toggle="modal" data-bs-target="#modal{{ $question->id }}">Xem trước</button>
                                    </td>
                                    <td><a wire:navigate href="" class="btn btn-sm btn-primary" href="">Sửa</a></td>
                                </tr>
                            @endforeach
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    @foreach ($questionCollection->questions as $question)
        <!-- Modal -->
        <div class="modal fade" id="modal{{ $question->id }}" tabindex="-1" aria-labelledby="modal{{ $question->id }}" aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered">
                <div class="modal-content  bg-secondary">
                    {{-- <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div> --}}
                    <div class="modal-body">
                        <div>
                            Câu hỏi: {{ $question->content }}
                            <hr />
                        </div>
                        @foreach ($question->answers as $answer)
                            <div>
                                - {{ $answer->content }}
                                @if ($answer->is_correct)
                                    <span class="text-success">
                                        (đúng)
                                    </span>
                                @endif
                            </div>
                        @endforeach

                    </div>
                    <div class="modal-footer my-1 py-1 align-items-center">
                        <button type="button" class="btn btn-outline-primary" data-bs-dismiss="modal">Thoát</button>
                    </div>
                </div>
            </div>
        </div>
    @endforeach
</div>
