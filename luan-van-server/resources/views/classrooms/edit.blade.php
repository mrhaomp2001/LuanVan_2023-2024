@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">

                        <h2>Chỉnh sửa lớp học</h2>
                        <div class="bg-secondary rounded p-4 ">
                            <form action="{{ route('classrooms.update') }}" method="POST">
                                @csrf
                                <div>
                                    <input type="hidden" name="classroom_id" value="{{ $classroom->id }}">
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="text" class="form-control" id="name" name="name"
                                        placeholder="Tên lớp" value="{{ $classroom->name }}">
                                    <label for="name">Tên lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <textarea class="form-control" placeholder="Miêu tả lớp học" name="description" id="description" style="height: 150px;">{{ $classroom->description }}</textarea>
                                    <label for="description">Miêu tả lớp học</label>
                                </div>
                                <button class="form-floating btn btn-info" type="submit">Sửa</button>
                            </form>
                        </div>

                        <hr />

                        <h2>Chỉnh sửa tài liệu của lớp học</h2>
                        <a href="{{ route('classrooms.documents.show', ['id' => $classroom->id]) }}"
                            class="btn btn-info">Đến
                            trang tài liệu học tập của lớp {{ $classroom->name }}</a>
                        <hr />

                        <h2>Các câu hỏi của lớp học</h2>
                        <div class="accordion" id="accordion">
                            @foreach ($questions as $question)
                                <div class="accordion-item bg-transparent">
                                    <h2 class="accordion-header" id="heading{{ $question->id }}">
                                        <button class="accordion-button collapsed bg-transparent text-light" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse{{ $question->id }}"
                                            aria-expanded="false" aria-controls="collapse{{ $question->id }}">
                                            ID {{ $question->id }}: {{ $question->content }}
                                        </button>
                                    </h2>
                                    <div id="collapse{{ $question->id }}" class="accordion-collapse collapse"
                                        aria-labelledby="heading{{ $question->id }}" data-bs-parent="#accordion">
                                        <div class="accordion-body row">
                                            Các câu trả lời của câu hỏi:
                                            <ul class="list-group list-group-flush">
                                                @foreach ($question->answers as $answer)
                                                    <li class="list-group-item bg-transparent d-flex justify-content-start">
                                                        - {{ $answer->content }}</li>
                                                @endforeach
                                            </ul>
                                            <button class="form-floating btn btn-success mt-3" type="submit">Sửa câu hỏi
                                                này</button>
                                        </div>
                                    </div>
                                </div>
                            @endforeach
                        </div>
                        <div class="mt-4">
                            {{ $questions->onEachSide(1)->links() }}
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Blank End -->
@stop
