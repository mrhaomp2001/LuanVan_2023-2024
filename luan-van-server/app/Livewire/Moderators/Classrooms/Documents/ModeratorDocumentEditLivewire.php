<?php

namespace App\Livewire\Moderators\Classrooms\Documents;

use App\Models\Classroom;
use App\Models\StudyDocument;
use Illuminate\Support\Facades\Storage;
use Livewire\Component;
use Livewire\WithFileUploads;


class ModeratorDocumentEditLivewire extends Component
{
    use WithFileUploads;

    public $classroom;
    public $studyDocument;
    public $content;
    public $image;
    public $image_path;
    public $page;

    public function mount($classroom_id, $study_document_id)
    {
        $this->classroom = Classroom::findOrFail($classroom_id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }
        $this->studyDocument = StudyDocument::findOrFail($study_document_id);
        if ($this->studyDocument->classroom_id != $this->classroom->id) {
            return redirect(route("404"));
        }

        $this->content = $this->studyDocument->content;
        $this->page = $this->studyDocument->page;
        $this->image_path = $this->studyDocument->image_path;
    }

    public function save()
    {
        $this->validate(
            [
                'content' => ["required", "min:3", 'max:512'],
                'image' => ['sometimes', 'nullable' ,'image']
            ],
            [
                'content.required' => "Cần nhập nội dung",
                'content.min' => "Cần nhập nội dung với tối thiểu :min ký tự",
                'content.max' => "Cần nhập nội dung với tối đa :max ký tự",
                'image.image' => "Cần nhập hình ảnh",

            ]
        );

        $this->studyDocument->content = $this->content;
        $this->studyDocument->save();

        if (isset($this->image)) {
            Storage::disk('public')->putFileAs("documents", $this->image, $this->studyDocument->id . '.png');
        }

        if ($this->page > $this->studyDocument->page) {

            $targetPage = 1;

            $documents = StudyDocument::where("classroom_id", $this->classroom->id)
                ->where("page", ">", $this->studyDocument->page)
                ->where("page", "<=", $this->page)
                ->get();

            foreach ($documents as $document) {
                $targetPage = $document->page;

                $document->page--;
                $document->save();
            }

            $this->studyDocument->page = $targetPage;
            $this->studyDocument->save();
        }

        if ($this->page < $this->studyDocument->page) {

            $targetPage = 1;

            $documents = StudyDocument::where("classroom_id", $this->classroom->id)
                ->where("page", "<", $this->studyDocument->page)
                ->where("page", ">=", $this->page)
                ->orderByDesc("page")
                ->get();

            foreach ($documents as $document) {
                $targetPage = $document->page;

                $document->page++;
                $document->save();
            }

            $this->studyDocument->page = $targetPage;
            $this->studyDocument->save();
        }

        return $this->redirect(route("moderator.classrooms.show", ['id' => $this->classroom->id]));
    }

    public function delete()
    {

        $documents = StudyDocument::where("classroom_id", $this->classroom->id)
            ->where("page", ">", $this->studyDocument->page)
            ->orderByDesc("page")
            ->get();

        foreach ($documents as $document) {
            $document->page--;
            $document->save();
        }

        Storage::disk('public')->delete('documents/' . $this->studyDocument->id . ".png");
        
        $this->studyDocument->delete();
        return $this->redirect(route("moderator.classrooms.show", ['id' => $this->classroom->id]));
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.documents.moderator-document-edit-livewire');
    }
}