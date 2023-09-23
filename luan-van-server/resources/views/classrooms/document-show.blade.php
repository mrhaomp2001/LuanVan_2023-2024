@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="d-flex justify-content-end mt-3">
                        <button class="btn btn-outline-info">+ Thêm trang</button>
                    </div>
                    <div class="bg-secondary rounded p-4 ">
                        @foreach ($documents as $document)
                            <h5>Trang {{ $document->page }}</h5>
                            <br />
                            <form action="{{ route("classrooms.documents.update") }}" method="post">
                                @csrf
                                <input type="hidden" name="study_document_id" value="{{ $document->id }}">

                                <div class="form-floating mb-3">
                                    <textarea class="form-control" placeholder="Nội dung của tài liệu học tập" name="content" id="content" style="height: 150px;">{{ $document->content }}</textarea>
                                    <label for="content">Nội dung của tài liệu học tập</label>
                                </div>

                                <button class="form-floating btn btn-info" type="submit">Sửa</button>
                            </form>
                        @endforeach
                    </div>
                    {{ $documents->onEachSide(1)->links() }}
                </div>
            </div>
        </div>
    </div>
    <!-- Blank End -->
@stop
