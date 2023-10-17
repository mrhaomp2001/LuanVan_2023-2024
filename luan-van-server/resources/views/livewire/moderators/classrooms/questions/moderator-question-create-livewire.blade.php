<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary rounded mb-3 p-3 d-inline-flex align-items-center">
            <div class="mx-2">
                <a wire:navigate href="{{ route('moderator.question-collections.show', ['classroom_id' => $classroom->id, 'question_collection_id' => $questionCollection->id]) }}" class="btn btn-square btn-primary ">
                    <i class="fa fa-arrow-left "></i>
                </a>
            </div>
            <div>
                Bạn đang làm việc với bộ câu hỏi <b>{{ $questionCollection->name }}</b> của lớp <b>{{ $classroom->name }}</b>
            </div>
        </div>
    </div>
    <div class="container-fluid px-4">
        <div class="bg-secondary rounded p-4 ">

            <h2>Thêm câu hỏi</h2>
            <form wire:submit="save">
                <div>
                    <h5>
                        Hãy nhập nội dung câu hỏi
                    </h5>
                </div>
                <div class="form-floating mb-3">
                    <input wire:model="questionContent" type="text" class="form-control" id="questionContent" name="questionContent" placeholder="nội dung câu hỏi" value="">
                    <label for="questionContent">Nội dung câu hỏi</label>
                    @error('questionContent')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>

                <hr />

                <div>
                    <div>
                        <h5>
                            Hãy nhập câu trả lời <span class="text-success">đúng</span>
                        </h5>
                    </div>
                    <div class="form-floating mb-3">
                        <input wire:model="answers.0" type="text" class="form-control" id="answers.0" name="answers.0" placeholder="nội dung câu trả lời" value="">
                        <label for="answers.0">Nội dung câu trả lời <b>đúng</b></label>
                        @error('answers.0')
                            <p class="text-start text-danger">
                                {{ $message }}
                            </p>
                        @enderror
                    </div>
                </div>

                <hr />
                <div>
                    <h5>
                        Hãy nhập các câu trả lời <span class="text-danger">không đúng</span>
                    </h5>
                </div>
                @for ($i = 1; $i < 4; $i++)
                    <div class="form-floating mb-3">
                        <input wire:model="answers.{{ $i }}" type="text" class="form-control" id="answers.{{ $i }}" name="answers.{{ $i }}" placeholder="nội dung câu trả lời" value="">
                        <label for="answers.{{ $i }}">Nội dung câu trả lời <b>không đúng</b></label>
                        @error('answers.{{ $i }}')
                            <p class="text-start text-danger">
                                {{ $message }}
                            </p>
                        @enderror
                    </div>
                @endfor
                <button class="form-floating btn btn-success" type="submit">+ Thêm</button>

            </form>
        </div>
    </div>
</div>
