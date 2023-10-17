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
        <div class="bg-secondary rounded p-4 ">
            <h2>Chỉnh sửa bộ câu hỏi</h2>
            <form wire:submit="save">
                <div class="form-floating mb-3">
                    <input wire:model="name" type="text" class="form-control" id="name" name="name" placeholder="Tên bộ câu hỏi" value="">
                    <label for="name">Tên bộ câu hỏi</label>
                    @error('name')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <div class="form-floating mb-3">
                    <input wire:model="difficulty" type="text" class="form-control" id="name" name="difficulty" placeholder="Miêu tả độ khó" value="">
                    <label for="difficulty">Miêu tả độ khó (khó, trung bình, dễ... mục đích hiển thị)</label>
                    @error('difficulty')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <div class="form-floating mb-3">
                    <input wire:model="questions_per_time" type="number" class="form-control" id="name" name="questions_per_time" placeholder="Miêu tả độ kh" value="">
                    <label for="questions_per_time">Số câu hỏi mỗi lần chơi (4 - 16 câu)</label>
                    @error('questions_per_time')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <div class=" mb-3">
                    <select wire:model="game_id" class="form-select mb-3" name="selectOpenClass">
                        <option value="0" disabled selected>Chọn 1 giá trị (bắt buộc)</option>
                        @foreach ($games as $game)
                            <option value="{{ $game->id }}">{{ $game->name }}</option>
                        @endforeach
                    </select>
                    @error('game_id')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <select wire:model="is_open" class="form-select mb-3" name="selectOpenClass">
                    <option value="none" disabled>Chọn 1 giá trị (tùy chọn)</option>
                    <option value="1">Mở bộ đề</option>
                    <option value="0">Đóng bộ đề</option>
                </select>
                @error('is_open')
                        <p class="text-start text-danger">
                            {{ $message . $is_open }}
                        </p>
                    @enderror
                <button class="form-floating btn btn-success" type="submit">+ Thêm</button>

            </form>
        </div>
    </div>
</div>
